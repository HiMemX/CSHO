using System.Collections.Generic;
using System.Linq;

namespace CSHO{
    public partial class Handler{
        public List<HoArchive.TOCEntry> GetAssets(){
            List<HoArchive.TOCEntry> assets = new List<HoArchive.TOCEntry>();
            foreach (HoArchive.ParcelBase parcel in GetParcels()){
                if (parcel is HoArchive.Parcel == false){continue;}

                foreach (HoArchive.ParcelTOC toc in ((HoArchive.Parcel)parcel).ParcelTOCs){
                    foreach (HoArchive.TOCEntry entry in toc.Entries){
                        assets.Add(entry);
                    }
                }
            }
            return assets;
        }

        public List<byte> GetAssetData(ulong AssetID){
            HoArchive.TOCEntry asset = GetAsset(AssetID);
            if (asset == null){return new List<byte>();}
            return asset.data;
        }

        public HoArchive.TOCEntry GetAsset(ulong AssetID){
            List<HoArchive.TOCEntry> assets = GetAssets();
            foreach (HoArchive.TOCEntry entry in assets){
                if (entry.uidSelf == AssetID){return entry;}
            }
            return null;
        }

        public List<HoArchive.TOCEntry> GetAssetsFromDataSnippet(List<byte> datasnippet){
            
            List<HoArchive.TOCEntry> assets = GetAssets();
            List<HoArchive.TOCEntry> output = new List<HoArchive.TOCEntry>();
            foreach (HoArchive.TOCEntry entry in assets){
                if(PartOfList(entry.data, datasnippet)){
                    output.Add(entry);
                }
            }
            return output;
        }


        public List<HoArchive.ParcelBase> GetParcels(HoArchive.Table table){
            List<HoArchive.ParcelBase> parcels = new List<HoArchive.ParcelBase>();
            foreach(HoArchive.ParcelBase parcel in table.Parcels){
                if(parcel is HoArchive.Table){
                    if(((HoArchive.Table)parcel).delete == true){continue;}

                    parcels.AddRange(GetParcels((HoArchive.Table)parcel));
                    continue;
                }

                parcels.Add(parcel);
            }
            return parcels;
        }

        public List<HoArchive.ParcelBase> GetParcels(){
            return GetParcels(Archive.MasterTable);
        }

        public List<HoArchive.ParcelBase> GetTables(HoArchive.Table table){
            List<HoArchive.ParcelBase> tables = new List<HoArchive.ParcelBase>();

            foreach(HoArchive.ParcelBase parcel in table.Parcels){
                if(parcel is HoArchive.Table){
                    if(((HoArchive.Table)parcel).delete == true){continue;}

                    tables.AddRange(GetTables((HoArchive.Table)parcel));
                    tables.Add(parcel);
                }
            }
            return tables;
        }

        public List<HoArchive.ParcelBase> GetTables(){
            return GetTables(Archive.MasterTable);
        }


        public List<HoArchive.NameTableEntry> GetNameEntries(){
            List<HoArchive.NameTableEntry> entries = new List<HoArchive.NameTableEntry>();
            foreach (HoArchive.ParcelBase parcel in GetParcels()){
                if (parcel is HoArchive.ParcelDebug == false){continue;}

                foreach (HoArchive.NameTableEntry entry in ((HoArchive.ParcelDebug)parcel).NameTableEntries){
                    entries.Add(entry);
                }
            }
            return entries;
        }

        public List<HoArchive.NameTableEntry> GetNameEntries(string name){
            List<HoArchive.NameTableEntry> entries = new List<HoArchive.NameTableEntry>();
            foreach (HoArchive.ParcelBase parcel in GetParcels()){
                if (parcel is HoArchive.ParcelDebug == false){continue;}

                foreach (HoArchive.NameTableEntry entry in ((HoArchive.ParcelDebug)parcel).NameTableEntries){
                    if(!entry.name.Contains(name)){continue;}
                    entries.Add(entry);
                }
            }
            return entries;
        }



        public HoArchive.NameTableEntry GetNameEntry(ulong AssetID){
            List<HoArchive.NameTableEntry> entries = GetNameEntries();
            foreach (HoArchive.NameTableEntry entry in entries){
                if (entry.uidAsset == AssetID){return entry;}
            }
            return null;
        }

        public string GetName(ulong AssetID){
            HoArchive.NameTableEntry entry = GetNameEntry(AssetID);
            if (entry == null){return "";}
            return entry.name;
        }

        
        

    }
}