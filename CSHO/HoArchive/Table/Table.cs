using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace HoArchive{
    public class Table : ParcelBase{ // Table is a type of Parcel (SECT, P, PD, PTEX, PFST). It will have the same methodes as normal parcels to allow for a complex tree (It also saves me some code).
        public TableHeader TableHeader;
        public List<TableEntry> TableEntries = new List<TableEntry>();
        public StringTable StringTable;
        public List<List<SliceMeta>> MetaTableEntries = new List<List<SliceMeta>>();
        public List<ParcelBase> Parcels = new List<ParcelBase>();

        public bool delete = false;

        public Table(string tableTypeTag, string DomainString, uint tableFlags = 0){
            TableHeader = new TableHeader(tableTypeTag, tableFlags);
            StringTable = new StringTable(DomainString);
        }

        public Table(BinaryReaderEndian file, uint sectorSize, string target, string platform){
            long baseposition = file.BaseStream.Position;

            // Table Header
            TableHeader = new TableHeader(file);

            // Table Entries
            for (int entry=0; entry<TableHeader.entryCount; entry++){
                TableEntries.Add(new TableEntry(file));
            }

            // String Table
            file.BaseStream.Position = TableHeader.firstString + baseposition;
            if(TableEntries.Count == 0){StringTable = new StringTable(file, TableHeader.stringTableSize);}
            else{StringTable = new StringTable(file, TableEntries[0].namePtr + baseposition);}
            

            // Meta Entries
            List<SliceMeta> tempentries = new List<SliceMeta>();
            foreach (TableEntry entry in TableEntries){
                if (entry.metaRecord != 0xFFFFFFFF){
                    file.BaseStream.Position = entry.metaRecord + baseposition;
                }

                tempentries = new List<SliceMeta>();
                for (int meta=0; meta<entry.metaBlockCount; meta++){
                    string type = file.ReadString(0x04);

                    if (type == "PSL\0"){
                        tempentries.Add(new ParcelSliceMeta(file));
                    }
                    else{
                        tempentries.Add(new ParcelDebugSliceMeta(file));
                    }
                }
                MetaTableEntries.Add(tempentries);
            }

            // Parcels
            for (int i=0; i<TableHeader.entryCount; i++){
                file.BaseStream.Position = TableEntries[i].startSector * sectorSize;
                switch (TableEntries[i].sectionType){
                    case "SECT":
                        Parcels.Add(new Table(file, sectorSize, target, platform));
                        break;
                    
                    case "PD  ":
                        Parcels.Add(new ParcelDebug(file, (ParcelDebugSliceMeta)MetaTableEntries[i][0]));
                        break;

                    default:
                        Parcels.Add(new Parcel(file, (ParcelSliceMeta)MetaTableEntries[i][0], target, platform));
                        break;
                }
            }
        }

        public void Update(uint Align, uint sectorSize, bool Reversed){
            for(int i=0; i<Parcels.Count; i++){
                if(TableEntries[i].sectionType == "SECT"){
                    if(!((Table)Parcels[i]).delete){continue;}
                }
                else if(TableEntries[i].sectionType == "PD  "){
                    if(!((ParcelDebug)Parcels[i]).delete){continue;}
                }
                else{
                    if(!((Parcel)Parcels[i]).delete){continue;}
                }
                TableEntries.RemoveAt(i);
                MetaTableEntries.RemoveAt(i);
                Parcels.RemoveAt(i);
                i = 0;
            }




            // Parcel (Parcel Content)
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                if (TableEntries[parcel].sectionType != "PD  " && TableEntries[parcel].sectionType != "SECT"){ // This weird stuff cause .hoes don't like being reversed
                    Parcels[parcel].Update(TableEntries[parcel].memoryAlignment, sectorSize, ((ParcelSliceMeta)MetaTableEntries[parcel][0]).Reversed);
                    continue;
                }
                Parcels[parcel].Update(TableEntries[parcel].memoryAlignment, sectorSize, false);
            }
            // ParcelMeta (Parcel Content description)
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                foreach (SliceMeta meta in MetaTableEntries[parcel]){
                    meta.Update(Parcels[parcel], TableEntries[parcel].memoryAlignment, sectorSize);
                }
            }
            // StringTable (We have no clue what it does, it's just there. We do know how it works doe)
            StringTable.Update();
            
            // TableEntry (Parcel Registry)
            uint StringTableLength = 0;
            foreach (string entry in StringTable.StringTableEntries){StringTableLength += MathTools.RoundUpTo(0x0D + (uint)entry.Length, 0x04);}
            uint namePtr = (uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength);

            uint size;
            uint currMetaRecord = MathTools.RoundUpTo((uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength + StringTable.DomainString.Length + 0x01), 0x20);
            uint nameHash = MathTools.LowerCaseBKDR(StringTable.DomainString);
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                size = Parcels[parcel].getSize(MetaTableEntries[parcel]);
                
                TableEntries[parcel].sectionType    = StringTools.ConditionalTrim(TableEntries[parcel].sectionType, 0x04);
                TableEntries[parcel].namePtr        = namePtr;
                TableEntries[parcel].sizeOnDisk     = size;
                TableEntries[parcel].sizeInMem      = size;
                TableEntries[parcel].metaBlockCount = (uint)MetaTableEntries[parcel].Count;
                TableEntries[parcel].nameHash       = nameHash;

                if (TableEntries[parcel].metaBlockCount == 0){
                    TableEntries[parcel].metaRecord = 0xFFFFFFFF;
                    continue;
                }

                TableEntries[parcel].metaRecord     = currMetaRecord;
                foreach(SliceMeta selfmeta in MetaTableEntries[parcel]){
                    if (selfmeta is ParcelSliceMeta){currMetaRecord += ((ParcelSliceMeta)selfmeta).Header.metaSize;}
                    else {currMetaRecord += ((ParcelDebugSliceMeta)selfmeta).Header.metaSize;}
                }
            }
            
            // TableHeader (Table description)
            uint MetaTableLength = 0;
            foreach (List<SliceMeta> metas in MetaTableEntries){
                foreach(SliceMeta selfmeta in metas){
                    if (selfmeta is ParcelSliceMeta){MetaTableLength += ((ParcelSliceMeta)selfmeta).Header.metaSize;}
                    else {MetaTableLength += ((ParcelDebugSliceMeta)selfmeta).Header.metaSize;}
                }
            }

            TableHeader.tableTypeTag    = StringTools.ConditionalTrim(TableHeader.tableTypeTag, 0x04);
            TableHeader.entryCount      = (uint)TableEntries.Count;
            TableHeader.firstString     = (uint)(TableEntries.Count * 0x40 + 0x20);
            TableHeader.stringTableSize = (uint)(StringTableLength + (StringTable.DomainString.Length + 0x01)*Convert.ToInt32(TableEntries.Count > 0));
            TableHeader.firstMetaRec    = MathTools.RoundUpTo((uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength + StringTable.DomainString.Length + 0x01), 0x20);
            TableHeader.metaDataSize    = MetaTableLength;
            
            if (TableHeader.metaDataSize == 0){
                TableHeader.firstMetaRec = 0xFFFFFFFF;
            }
        }

        public void updateSectors(uint sectorSize, uint startSector){
            uint currSector = startSector + MathTools.CeilDiv(MathTools.RoundUpTo(TableHeader.firstString + TableHeader.stringTableSize, 0x20) + TableHeader.metaDataSize, sectorSize);
            

            for (int parcel=0; parcel<TableHeader.entryCount; parcel++){
                Parcels[parcel].updateSectors(sectorSize, currSector);
                TableEntries[parcel].startSector = currSector;
                currSector += Parcels[parcel].getTotalSize(MetaTableEntries[parcel], sectorSize) / sectorSize;
            }
        }

        public uint getSize(List<SliceMeta> meta){
            uint StringTableLength = 0;
            uint MetaTableLength = 0;
            foreach (string entry in StringTable.StringTableEntries){StringTableLength += 0x0D + (uint)entry.Length;}
            foreach (List<SliceMeta> metas in MetaTableEntries){
                foreach(SliceMeta selfmeta in metas){
                    if (selfmeta is ParcelSliceMeta){MetaTableLength += ((ParcelSliceMeta)selfmeta).Header.metaSize;}
                    else {MetaTableLength += ((ParcelDebugSliceMeta)selfmeta).Header.metaSize;}
                }
            }
            uint size = MathTools.RoundUpTo((uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength + (StringTable.DomainString.Length + 0x01)*Convert.ToInt32(TableEntries.Count > 0)), 0x20) + MetaTableLength;
            return size;
        }

        public uint getTotalSize(List<SliceMeta> meta, uint sectorSize){
            uint size = getSize(meta);
            size = MathTools.RoundUpTo(size, sectorSize);
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                size += Parcels[parcel].getTotalSize(MetaTableEntries[parcel], sectorSize);
            }
            return size;
        }


        public void Save(BinaryWriterEndian file, uint sectorSize, List<SliceMeta> selfmetas){
            TableHeader.Save(file);

            foreach (TableEntry entry in TableEntries){
                entry.Save(file);
            }

            StringTable.Save(file, TableEntries.Count > 0);
            file.PadAlign(0x20, 0x33);

            foreach (List<SliceMeta> metas in MetaTableEntries){
                foreach (SliceMeta meta in metas){
                    meta.Save(file);
                }
            }

            file.PadAlign(sectorSize, 0x33);

            for (int i=0; i<TableHeader.entryCount; i++){
                file.PadTo(TableEntries[i].startSector*sectorSize, 0x33);
                Parcels[i].Save(file, sectorSize, MetaTableEntries[i]);
            }
        }

        public void SaveLSET(StreamWriter file, string indent, string tag, List<NameTableEntry> nameTableEntries, TableEntry entry=null){
            string args = "tableFlags=" + TableHeader.tableFlags;
            if(entry != null){args += ", " + entry.getArgs();}
            file.WriteLine(indent + tag + "(" + args + "){");
            StringTable.SaveLSET(file, indent + "   ");
            file.WriteLine();
            

            for (int i=0; i<TableHeader.entryCount; i++){
                if(Parcels[i] is ParcelDebug){continue;}
                Parcels[i].SaveLSET(file, indent + "   ", TableEntries[i].sectionType, nameTableEntries, entry: TableEntries[i]);
                file.WriteLine();
            }

            file.WriteLine(indent + "}");
        }

        public Table(List<string> lines, string tableTypeTag, string game, string platform, string assetpath, uint tableFlags=0){
            TableHeader = new TableHeader(tableTypeTag, tableFlags);

            List<string> stringTableLines = new List<string>();
            bool gotStringTable = false;
            string DomainString = "";
            foreach(string line in lines){
                if(gotStringTable){
                    if(line == "}"){break;}
                    stringTableLines.Add(line);
                    continue;
                }
                if(line.Length < 12){continue;}
                if(line.Substring(0, 12) != "StringTable("){continue;}
                gotStringTable = true;
                DomainString = (line.Substring(12, line.Length-14).Replace(" ", "").Split("=")[1].Replace("'","").Replace("\"",""));
            }
            StringTable = new StringTable(DomainString);
            stringTableLines.Remove("\n");
            StringTable.StringTableEntries = stringTableLines;

            string tag;
            int lineindex = 0;
            List<string> linebuffer = new List<string>();
            List<string> argbuffer;
            
            uint newtableFlags = 0;
            LanguageID packLangID = LanguageID.Neutral;
            enParcelType parcelType = enParcelType.PARCEL_TYPE_EXCLUSIVE;
            uint fromNameHash = 0;
            uint attributeFlags = 0;
            uint externName = 0xFFFFFFFF;
            ParcelDebug exclusive = null;
            ParcelDebug shared = null;
            ParcelDebug fromDomain = null;
            ParcelDebug currDebugParcel = null;

            foreach(string line in lines){ // Parcel Fetching
                lineindex ++;
                if(linebuffer.Count > 0){
                    linebuffer.RemoveAt(0);
                    continue;
                }
                if(line.Length < 4){
                    continue;
                }
                tag = line.Split("(")[0].Replace(" ", "");
                if(tag != "SECT" && tag != "P" && tag != "PTEX" && tag != "PFST"){continue;}

                linebuffer = StringTools.ReadUntilCloseBracket(lines.ToArray()[(lineindex)..^0].ToList());
                argbuffer = StringTools.GetArgs(line);
                
                //lineindex += linebuffer.Count;

                //Console.WriteLine(tag);
                //Console.WriteLine(lines.ToArray()[(lineindex)..^0].ToList()[0]);
                //Console.WriteLine(String.Join(", ", argbuffer));

                if(StringTools.GetArg(argbuffer, "tableFlags") != null){newtableFlags = uint.Parse(StringTools.GetArg(argbuffer, "tableFlags"));}
                if(StringTools.GetArg(argbuffer, "packLangID") != null){packLangID = (LanguageID)Enum.Parse(typeof(LanguageID), StringTools.GetArg(argbuffer, "packLangID"));}
                if(StringTools.GetArg(argbuffer, "parcelType") != null){parcelType = (enParcelType)Enum.Parse(typeof(enParcelType), StringTools.GetArg(argbuffer, "parcelType"));}
                if(StringTools.GetArg(argbuffer, "fromNameHash") != null){fromNameHash = uint.Parse(StringTools.GetArg(argbuffer, "fromNameHash"));}
                if(StringTools.GetArg(argbuffer, "attributeFlags") != null){attributeFlags = uint.Parse(StringTools.GetArg(argbuffer, "attributeFlags"));}
                if(StringTools.GetArg(argbuffer, "externName") != null){externName = uint.Parse(StringTools.GetArg(argbuffer, "externName"));}
                
                if(parcelType == enParcelType.PARCEL_TYPE_EXCLUSIVE && exclusive == null){
                    TableEntries.Add(new HoArchive.TableEntry("PD  ", packLangID, parcelType, fromNameHash_in: fromNameHash, attributeFlags_in: attributeFlags, externName_in: externName));
                    MetaTableEntries.Add(new List<SliceMeta>(){new ParcelDebugSliceMeta()});
                    Parcels.Add(new HoArchive.ParcelDebug());
                    exclusive = (ParcelDebug)Parcels.Last();

                }
                if(parcelType == enParcelType.PARCEL_TYPE_SHARED && shared == null){
                    TableEntries.Add(new HoArchive.TableEntry("PD  ", packLangID, parcelType, fromNameHash_in: fromNameHash, attributeFlags_in: attributeFlags, externName_in: externName));
                    MetaTableEntries.Add(new List<SliceMeta>(){new ParcelDebugSliceMeta()});
                    Parcels.Add(new HoArchive.ParcelDebug());
                    shared = (ParcelDebug)Parcels.Last();
                }
                if(parcelType == enParcelType.PARCEL_TYPE_FROMDOMAIN && fromDomain == null){
                    TableEntries.Add(new HoArchive.TableEntry("PD  ", packLangID, parcelType, fromNameHash_in: fromNameHash, attributeFlags_in: attributeFlags, externName_in: externName));
                    MetaTableEntries.Add(new List<SliceMeta>(){new ParcelDebugSliceMeta()});
                    Parcels.Add(new HoArchive.ParcelDebug());
                    fromDomain = (ParcelDebug)Parcels.Last();
                }

                if(parcelType == enParcelType.PARCEL_TYPE_EXCLUSIVE){currDebugParcel = exclusive;}
                if(parcelType == enParcelType.PARCEL_TYPE_SHARED){currDebugParcel = shared;}
                if(parcelType == enParcelType.PARCEL_TYPE_FROMDOMAIN){currDebugParcel = fromDomain;}

                TableEntries.Add(new TableEntry(tag.PadRight(4, ' '), packLangID, parcelType, fromNameHash_in: fromNameHash, attributeFlags_in: attributeFlags, externName_in: externName));
                
                if(tag == "SECT"){
                    MetaTableEntries.Add(new List<SliceMeta>());
                    Parcels.Add(new Table(linebuffer, tag, game, platform, assetpath, newtableFlags));
                }
                else{
                    MetaTableEntries.Add(new List<SliceMeta>(){new ParcelSliceMeta(game=="WALE" && platform=="WII")});
                    Parcels.Add(new Parcel(linebuffer, currDebugParcel, game, platform, assetpath));
                }
                

            }
        }
    }
}