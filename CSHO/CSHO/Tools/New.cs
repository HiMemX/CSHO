using System;
using System.Collections.Generic;
using System.Linq;
using HoArchive;

namespace CSHO{
    public partial class Handler{
        public string NewSection(HoArchive.Table table, string DomainString){
            table.TableEntries.Add(new HoArchive.TableEntry("SECT"));
            table.MetaTableEntries.Add(new List<HoArchive.SliceMeta>());
            table.Parcels.Add(new HoArchive.Table("SECT", DomainString));
            return "";
        }


        // ------------ Most important ---------------- //

        public string NewParcel(HoArchive.Table table, 
                        string sectionType_in, 
                        HoArchive.LanguageID packLangID_in = HoArchive.LanguageID.Neutral, 
                        HoArchive.enParcelType parcelType_in = HoArchive.enParcelType.PARCEL_TYPE_EXCLUSIVE, 
                        uint userKey_in = 0, 
                        uint fromNameHash_in = 0, 
                        uint fromNamePtr_in = 0xFFFFFFFF,
                        uint attributeFlags_in = 0,
                        uint externName_in = 0xFFFFFFFF,
                        bool Reversed = false){
            table.TableEntries.Add(new HoArchive.TableEntry(sectionType_in, packLangID_in, parcelType_in, userKey_in, fromNameHash_in, fromNamePtr_in, attributeFlags_in, externName_in));
            table.MetaTableEntries.Add(new List<HoArchive.SliceMeta>(){new HoArchive.ParcelSliceMeta(Reversed)});
            table.Parcels.Add(new HoArchive.Parcel());

            return "";
        }

        public string NewParcelDebug(HoArchive.Table table,
                        HoArchive.LanguageID packLangID_in = HoArchive.LanguageID.Neutral, 
                        HoArchive.enParcelType parcelType_in = HoArchive.enParcelType.PARCEL_TYPE_EXCLUSIVE, 
                        uint userKey_in = 0, 
                        uint fromNameHash_in = 0, 
                        uint fromNamePtr_in = 0xFFFFFFFF,
                        uint attributeFlags_in = 0,
                        uint externName_in = 0xFFFFFFFF,
                        bool Reversed = false){
            table.TableEntries.Add(new HoArchive.TableEntry("PD  ", packLangID_in, parcelType_in, userKey_in, fromNameHash_in, fromNamePtr_in, attributeFlags_in, externName_in));
            table.MetaTableEntries.Add(new List<HoArchive.SliceMeta>(){new HoArchive.ParcelDebugSliceMeta()});
            table.Parcels.Add(new HoArchive.ParcelDebug());

            return "";
        }

        public string NewParcelTOC(HoArchive.Parcel parcel){
            parcel.ParcelTOCs.Add(new HoArchive.ParcelTOC());
        
            return "";
        }

        public string NewAsset(HoArchive.ParcelTOC toc, ulong uidSelf_in, HoArchive.wmlTypeID wmlTypeID_in, uint blobAlign_in = 0x04, ushort subType_in = 0, ushort blobFlags_in = 1, List<byte> data_in = null, Asset.AssetEntity entity_in = null){
            toc.Entries.Add(new HoArchive.TOCEntry(uidSelf_in, wmlTypeID_in, blobAlign_in, subType_in, blobFlags_in, data_in, entity_in));
            
            return "";
        }

        public string NewAsset(HoArchive.ParcelTOC toc, ulong uidSelf_in, HoArchive.wmlTypeID wmlTypeID_in, string path, uint blobAlign_in = 0x04, ushort subType_in = 0, ushort blobFlags_in = 1){
            toc.Entries.Add(new HoArchive.TOCEntry(uidSelf_in, wmlTypeID_in, data_in: ReadFile(path)));
            
            return "";
        }

        public string NewNameTableEntry(HoArchive.ParcelDebug parcel, ulong uidAsset_in, string name_in, uint typeID_in = 0xFFFFFFFF){
            parcel.NameTableEntries.Add(new HoArchive.NameTableEntry(uidAsset_in, name_in, typeID_in));

            return "";
        }

        public string NewNameTableEntry(ulong uidAsset, string name, uint typeID = 0xFFFFFFFF)
        {
            ParcelDebug parcel = GetAvailableParcelDebug();

            return NewNameTableEntry(parcel, uidAsset, name, typeID);
        }

        public string NewAsset(TOCEntry entry, string targetparcel = "P", int targettoc = 0) // Function to add an already existing TOCEntry to a certain parcel + table combo
        {
            Parcel parcel = (Parcel)GetFirstMatchingParcel(targetparcel);
            if (parcel == null) return "ERR_NO_MATCHING_PARCEL_AVAILABLE";
            if (targettoc >= parcel.ParcelTOCs.Count) return "ERR_NO_MATCHING_TOC_AVAILABLE";

            parcel.ParcelTOCs[targettoc].AddTOCEntry(entry);

            return "";
        }

        public string NewAssets(List<TOCEntry> entries, string targetparcel = "P", int targettoc = 0) // Function to add an already existing TOCEntry to a certain parcel + table combo
        {
            Parcel parcel = (Parcel)GetFirstMatchingParcel(targetparcel);
            if (parcel == null) return "ERR_NO_MATCHING_PARCEL_AVAILABLE";
            if (targettoc >= parcel.ParcelTOCs.Count) return "ERR_NO_MATCHING_TOC_AVAILABLE";

            parcel.ParcelTOCs[targettoc].AddTOCEntries(entries);

            return "";
        }

        // Honestly what was I smoking when I wrote all of this ^
        // - Me, 25.08.2025

        // Alright time to smoke some more
        public TOCEntry DuplicateFirstOccurence(ulong uid, bool newname = true)
        {
            foreach (ParcelTOC toc in GetParcelTOCs())
            {
                TOCEntry entry = toc.GetEntry(uid);

                if (entry == null) continue;

                // Duplicator code here
                ParcelDebug debugParcel = GetAvailableParcelDebug();

                if (debugParcel == null)
                {
                    throw new Exception("Couldn't find ParcelDebug for duplication operation");
                }

                TOCEntry newentry = entry.DeepCopy();
                newentry.uidSelf = GenerateAssetID();
                newentry.CreateEntity(new MemoryStreamEndian(newentry.data.ToArray(), endian), Archive.Header.target, Archive.Header.platform);
                newentry.Update();


                string name = GetName(uid);
                if (newname) name = String.Join("_", name.Split("_").SkipLast(1)) + "_" + (GetNameCount(name) + 1).ToString();

                debugParcel.AddEntry(newentry.uidSelf, name);
                toc.AddTOCEntry(newentry);

                return newentry;
            }
            return null;
        }
    }
}