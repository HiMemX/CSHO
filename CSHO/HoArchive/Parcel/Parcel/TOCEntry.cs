using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel;
using System.IO;

namespace HoArchive{
    public class TOCEntry{
        [ReadOnly(true)]
        public uint elementSize {get; set;}
        [ReadOnly(true)]
        public uint elementOffset {get; set;}
        [ReadOnly(true)]
        public uint blobSize {get; set;}
        public uint blobAlign {get; set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong uidSelf {get; set;}
        [ReadOnly(true)]
        [TypeConverter(typeof(wmlTypeIDConverter))]
        public wmlTypeID wmlTypeID {get; set;}
        public ushort subType {get; set;}
        public ushort blobFlags {get; set;}

        public Asset.AssetEntity entity;

        public List<byte> data;

        public bool delete = false;

        public TOCEntry(ulong uidSelf_in, wmlTypeID wmlTypeID_in, uint blobAlign_in = 0x04, ushort subType_in = 0, ushort blobFlags_in = 1, List<byte> data_in = null, Asset.AssetEntity entity_in = null){
            if(data_in == null && entity == null){throw new Exception();}
            
            uidSelf = uidSelf_in;
            wmlTypeID = wmlTypeID_in;
            blobAlign = blobAlign_in;
            subType = subType_in;
            blobFlags = blobFlags_in;

            if(data_in != null){
                data = data_in;
            }
            if(entity_in != null){
                entity = entity_in;
            }
        }

        public TOCEntry(BinaryReaderEndian file, uint DataPtr, string target, string platform){
            elementSize   = file.ReadUInt32E();
            elementOffset = file.ReadUInt32E();
            blobSize      = file.ReadUInt32E();
            blobAlign     = file.ReadUInt32E();
            uidSelf       = file.ReadUInt64E();
            wmlTypeID     = (wmlTypeID)file.ReadUInt32E();
            subType       = file.ReadUInt16E();
            blobFlags     = file.ReadUInt16E();

            uint returnaddr = (uint)file.BaseStream.Position;
            file.BaseStream.Position = DataPtr;
            data = new List<byte>(file.ReadBytes((int)blobSize));
            file.BaseStream.Position = DataPtr;
            MemoryStreamEndian stream = new MemoryStreamEndian(data.ToArray(), file.endianness);
            entity = Asset.AssetCaster.ReadAsset(stream, wmlTypeID, target, platform);
            stream.Dispose();

            file.BaseStream.Position = returnaddr;
        }

        public void Update(uint Align){
            // This code blob is just to update the data length. If the statement is true then the data member won't be saved anyways.
            MemoryStreamEndian stream = new MemoryStreamEndian(false);
            if (entity != null && Asset.AvailableAssetsTEMP.available.Contains(wmlTypeID)){
                entity.Update(this);
                entity.Save(stream);
                data = stream.ToArray().ToList<byte>();
            }
            stream.Dispose();
            //
            elementSize   = MathTools.RoundUpTo((uint)data.Count, Align);
            blobSize      = (uint)data.Count;
        }

        public void SaveData(BinaryWriterEndian file){
            file.Write(data.ToArray());
            file.Pad(elementSize - blobSize, 0x33);
        }

        public void SaveMeta(BinaryWriterEndian file){
            file.WriteE(elementSize);
            file.WriteE(elementOffset);
            file.WriteE(blobSize);
            file.WriteE(blobAlign);
            file.WriteE(uidSelf);
            file.WriteE((uint)wmlTypeID);
            file.WriteE(subType);
            file.WriteE(blobFlags);
        }

        public TOCEntry Copy(){
            return (TOCEntry)this.MemberwiseClone();
        }

        public string Serialize(){
            return blobAlign + ", " + uidSelf + ", " + (uint)wmlTypeID + ", " + subType + ", " + blobFlags + ", " + System.Convert.ToBase64String(data.ToArray());
        }

        public void SaveLSET(StreamWriter file, string indent, List<NameTableEntry> nameTableEntries){
            string name = "unknown";
            foreach(HoArchive.NameTableEntry entry in nameTableEntries){
                
                if (entry.uidAsset == uidSelf){name = entry.name; break;}
            }
            string newname = "";
            foreach(string bit in name.Replace("_", ".").Split(".")){
                if(bit == "tif"){newname += "." + bit;}
                else{
                    if (newname!=""){newname += "/";}
                    newname += bit;
                }
            }
            //string path = wmlTypeID.ToString() + newname + " [" + System.Convert.ToHexString(BitConverter.GetBytes(uidSelf).Reverse().ToArray()) + "]" +".dat";
            //string path = newname + " [" + System.Convert.ToHexString(BitConverter.GetBytes(uidSelf).Reverse().ToArray()) + "]" +".dat";
            List<string> dirlist = newname.Split("/").ToList();
            dirlist.RemoveAt(dirlist.Count - 1);
            string dir = String.Join("/", dirlist);
            
            //string path = currfoldername.Split("_")[0] + "/" + String.Join("_", currfoldername.Split("_")[1..^0]) + "/[ASSET] " + wmlTypeID.ToString() + "/" + name  + " [" + System.Convert.ToHexString(BitConverter.GetBytes(uidSelf).Reverse().ToArray()) + "]" +".dat";
            string path;

            if(dir == ""){path = "[ASSETS] " + wmlTypeID.ToString() + "/" + newname.Split("/").Last() + " [" + System.Convert.ToHexString(BitConverter.GetBytes(uidSelf).Reverse().ToArray()) + "]" +".dat";}
            else{path = dir + "/[ASSETS] " + wmlTypeID.ToString() + "/" + newname.Split("/").Last() + " [" + System.Convert.ToHexString(BitConverter.GetBytes(uidSelf).Reverse().ToArray()) + "]" +".dat";}
            file.WriteLine(indent + "$" + System.Convert.ToHexString(BitConverter.GetBytes(uidSelf).Reverse().ToArray()) + " " + path.PadRight(120, ' ') + "	" + wmlTypeID.ToString().PadRight(20) + "	blobAlign=" + blobAlign + "	subType=" + subType + "	blobFlags=" + blobFlags);
        }

        public TOCEntry(string line, ParcelDebug debugParcel, string game, string platform, string assetpath){
            string path = line.Substring(line.IndexOf(' ')+1, line.IndexOf(".dat")-line.IndexOf(' ')+3);
            uidSelf = Convert.ToUInt64(line.Split(" ")[0].Substring(1), 16);
            line = StringTools.RemovePaddingSpaces(line.Replace(line.Substring(0, line.IndexOf(".dat")+5), ""));
        
            List<string> args = line.Split(" ").ToList();
            wmlTypeID = (wmlTypeID)Enum.Parse(typeof(wmlTypeID), args[0]);
            args.RemoveAt(0);

            blobAlign = 4;
            subType = 0;
            blobFlags = 1;

            foreach(string arg in args){
                if(arg.Split("=")[0] == "blobAlign"){blobAlign = uint.Parse(arg.Split("=")[1].Replace(" ", ""));}
                if(arg.Split("=")[0] == "subType"){subType = ushort.Parse(arg.Split("=")[1].Replace(" ", ""));}
                if(arg.Split("=")[0] == "blobFlags"){blobFlags = ushort.Parse(arg.Split("=")[1].Replace(" ", ""));}
            }
            
            HoArchive.BinaryReaderEndian file = null;
            try{
                file = new HoArchive.BinaryReaderEndian(assetpath + "/" + path, false);
                data = file.ReadBytes((int)file.BaseStream.Length).ToList();
                try{
                    MemoryStreamEndian stream = new MemoryStreamEndian(data.ToArray(), file.endianness);
                    entity = Asset.AssetCaster.ReadAsset(stream, wmlTypeID, game, platform);
                    stream.Dispose();
                }
                catch{
                    Console.WriteLine("Failed to form entity: " + path);
                }
                file.Dispose();
            }
            catch(FileNotFoundException){Console.WriteLine("Couldn't find: " +path); }

            debugParcel.NameTableEntries.Add(new NameTableEntry(uidSelf, path.Replace("\\", "/").Replace("/", "_").Replace(".dat", "")));

        }
    }
}