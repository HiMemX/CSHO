using System.Collections.Generic;
using System.Linq;

namespace CSHO{
    public partial class Handler{
        bool FetchEndian(HoArchive.BinaryReaderEndian file){
            file.BaseStream.Position = 0x06;
            bool endian = (file.ReadUInt16E() == 0);
            file.BaseStream.Position = 0x00;    
            return endian;
        }

        bool FetchValid(HoArchive.BinaryReaderEndian file){
            file.BaseStream.Position = 0x04;
            bool valid = (file.ReadUInt32E() != 0);
            file.BaseStream.Position = 0x00;
            return valid;
        }



        public List<HoArchive.TOCEntry> GetAssets(){
            List<HoArchive.TOCEntry> assets = new List<HoArchive.TOCEntry>();
            foreach (HoArchive.ParcelBase table in Archive.MasterTable.Parcels){
                foreach (HoArchive.ParcelBase parcel in ((HoArchive.Table)table).Parcels){
                    if (parcel is HoArchive.Parcel == false){continue;}

                    foreach (HoArchive.ParcelTOC toc in ((HoArchive.Parcel)parcel).ParcelTOCs){
                        foreach (HoArchive.TOCEntry entry in toc.Entries){
                            assets.Add(entry);
                        }
                    }
                }
            }
            return assets;
        }


        public string SetAssetData(byte[] AssetID, List<byte> data){
            HoArchive.TOCEntry asset = GetAsset(AssetID);
            if (asset == null){return "ERR_ASSET_NOT_FOUND";}
            asset.data = data;
            return "";
        }

        public List<byte> GetAssetData(byte[] AssetID){
            HoArchive.TOCEntry asset = GetAsset(AssetID);
            if (asset == null){return new List<byte>();}
            return asset.data;
        }

        public HoArchive.TOCEntry GetAsset(byte[] AssetID){
            List<HoArchive.TOCEntry> assets = GetAssets();
            foreach (HoArchive.TOCEntry entry in assets){
                if (entry.uidSelf.SequenceEqual(AssetID)){return entry;}
            }
            return null;
        }

        public byte[] GenerateAssetID(){
            byte[] AssetID = new byte[8];
            
            while (true){
                RNG.NextBytes(AssetID); 
                if (GetAsset(AssetID) == null){return AssetID;}
            }
        }



        public List<HoArchive.NameTableEntry> GetNameEntries(){
            List<HoArchive.NameTableEntry> entries = new List<HoArchive.NameTableEntry>();
            foreach (HoArchive.ParcelBase table in Archive.MasterTable.Parcels){
                foreach (HoArchive.ParcelBase parcel in ((HoArchive.Table)table).Parcels){
                    if (parcel is HoArchive.ParcelDebug == false){continue;}

                    foreach (HoArchive.NameTableEntry entry in ((HoArchive.ParcelDebug)parcel).NameTableEntries){
                        entries.Add(entry);
                    }
                }
            }
            return entries;
        }

        public HoArchive.NameTableEntry GetNameEntry(byte[] AssetID){
            List<HoArchive.NameTableEntry> entries = GetNameEntries();
            foreach (HoArchive.NameTableEntry entry in entries){
                if (entry.uidAsset.SequenceEqual(AssetID)){return entry;}
            }
            return null;
        }

        public string GetName(byte[] AssetID){
            HoArchive.NameTableEntry entry = GetNameEntry(AssetID);
            if (entry == null){return null;}
            return entry.name;
        }
    }
}