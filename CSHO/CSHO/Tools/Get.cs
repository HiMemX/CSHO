using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HoArchive;

namespace CSHO{
    public partial class Handler
    {

        public List<HoArchive.ParcelTOC> GetParcelTOCs()
        {
            List<HoArchive.ParcelTOC> tocs = new List<HoArchive.ParcelTOC>();
            foreach (HoArchive.ParcelBase parcel in GetParcels())
            {
                if (parcel is HoArchive.Parcel == false) { continue; }

                tocs.AddRange(((HoArchive.Parcel)parcel).ParcelTOCs);

            }
            return tocs;

        }

        public List<HoArchive.TOCEntry> GetAssets(enParcelType? targetType = null)
        {
            List<HoArchive.TOCEntry> assets = new List<HoArchive.TOCEntry>();
            foreach (HoArchive.ParcelBase parcel in GetParcels(targetType))
            {

                if (parcel is HoArchive.Parcel == false) { continue; }

                foreach (HoArchive.ParcelTOC toc in ((HoArchive.Parcel)parcel).ParcelTOCs)
                {
                    foreach (HoArchive.TOCEntry entry in toc.Entries)
                    {
                        assets.Add(entry);
                    }
                }
            }
            return assets;
        }

        public List<HoArchive.TOCEntry> GetAssets(List<ulong> ids)
        {
            List<HoArchive.TOCEntry> output = new();
            foreach (ulong id in ids)
            {
                output.Add(GetAsset(id));
            }


            return output;
        }

        public List<byte> GetAssetData(ulong AssetID)
        {
            HoArchive.TOCEntry asset = GetAsset(AssetID);
            if (asset == null) { return new List<byte>(); }
            return asset.data;
        }

        public HoArchive.TOCEntry GetAsset(ulong AssetID)
        {
            if (AssetID == 0) { return null; }

            List<HoArchive.TOCEntry> assets = GetAssets();
            foreach (HoArchive.TOCEntry entry in assets)
            {
                if (entry.uidSelf == AssetID) { return entry; }
            }
            return null;
        }

        public List<HoArchive.TOCEntry> GetAssetsFromDataSnippet(List<byte> datasnippet)
        {

            List<HoArchive.TOCEntry> assets = GetAssets();
            List<HoArchive.TOCEntry> output = new List<HoArchive.TOCEntry>();
            foreach (HoArchive.TOCEntry entry in assets)
            {
                if (PartOfList(entry.data, datasnippet))
                {
                    output.Add(entry);
                }
            }
            return output;
        }


        public List<HoArchive.ParcelBase> GetParcels(HoArchive.Table table, enParcelType? targetType = null)
        {
            List<HoArchive.ParcelBase> parcels = new List<HoArchive.ParcelBase>();

            int index = -1; // fuck it
            foreach (HoArchive.ParcelBase parcel in table.Parcels)
            {
                index++;
                if (parcel is HoArchive.Table)
                {

                    if (((HoArchive.Table)parcel).delete == true) { continue; }

                    parcels.AddRange(GetParcels((HoArchive.Table)parcel, targetType));
                    continue;
                }

                if (targetType != null && (targetType != table.TableEntries[index].parcelType)) continue;

                parcels.Add(parcel);
            }
            return parcels;
        }

        public List<HoArchive.ParcelBase> GetParcels(enParcelType? targetType = null)
        {
            return GetParcels(Archive.MasterTable, targetType);
        }

        public List<HoArchive.ParcelBase> GetTables(HoArchive.Table table)
        {
            List<HoArchive.ParcelBase> tables = new List<HoArchive.ParcelBase>();

            foreach (HoArchive.ParcelBase parcel in table.Parcels)
            {
                if (parcel is HoArchive.Table)
                {
                    if (((HoArchive.Table)parcel).delete == true) { continue; }

                    tables.AddRange(GetTables((HoArchive.Table)parcel));
                    tables.Add(parcel);
                }
            }
            return tables;
        }

        public List<HoArchive.ParcelBase> GetTables()
        {
            return GetTables(Archive.MasterTable);
        }


        public List<HoArchive.NameTableEntry> GetNameEntries()
        {
            List<HoArchive.NameTableEntry> entries = new List<HoArchive.NameTableEntry>();
            foreach (HoArchive.ParcelBase parcel in GetParcels())
            {
                if (parcel is HoArchive.ParcelDebug == false) { continue; }

                foreach (HoArchive.NameTableEntry entry in ((HoArchive.ParcelDebug)parcel).NameTableEntries)
                {
                    entries.Add(entry);
                }
            }
            return entries;
        }

        public List<HoArchive.NameTableEntry> GetNameEntries(string name)
        {
            List<HoArchive.NameTableEntry> entries = new List<HoArchive.NameTableEntry>();
            foreach (HoArchive.ParcelBase parcel in GetParcels())
            {
                if (parcel is HoArchive.ParcelDebug == false) { continue; }

                foreach (HoArchive.NameTableEntry entry in ((HoArchive.ParcelDebug)parcel).NameTableEntries)
                {
                    if (!entry.name.Contains(name)) { continue; }
                    entries.Add(entry);
                }
            }
            return entries;
        }



        public HoArchive.NameTableEntry GetNameEntry(ulong AssetID)
        {
            List<HoArchive.NameTableEntry> entries = GetNameEntries();
            foreach (HoArchive.NameTableEntry entry in entries)
            {
                if (entry.uidAsset == AssetID) { return entry; }
            }
            return null;
        }

        public string GetName(ulong AssetID)
        {
            HoArchive.NameTableEntry entry = GetNameEntry(AssetID);
            if (entry == null) { return ""; }
            return entry.name;
        }

        public int GetNameCount(string name, bool ignoreNumbers = true)
        {
            int runningtally = 0;
            foreach (ParcelBase parcel in GetParcels())
            {
                if (!(parcel is ParcelDebug)) continue;

                foreach (NameTableEntry entry in ((ParcelDebug)parcel).NameTableEntries)
                {
                    if (Regex.Replace(name, @"\d", "") == Regex.Replace(entry.name, @"\d", "")) runningtally++;
                }
            }
            return runningtally;
        }

        public ParcelDebug GetAvailableParcelDebug()
        {
            ParcelDebug parcelDebug = null;


            foreach (HoArchive.ParcelBase parcel in GetParcels())
            {
                if (parcel is HoArchive.ParcelDebug) { if (((HoArchive.ParcelDebug)parcel).delete != true) { parcelDebug = (HoArchive.ParcelDebug)parcel; break; } }
            }

            return parcelDebug;
        }

        public ParcelBase GetFirstMatchingParcel(string parceltag)
        {
            List<ParcelBase> tables = GetTables();


            foreach (Table table in tables)
            {
                for (int p = 0; p < table.Parcels.Count; p++)
                {
                    TableEntry entry = table.TableEntries[p];

                    if (entry.sectionType.Replace(" ", "") == parceltag.Replace(" ", "")) return table.Parcels[p];
                }
            }

            // No matching parcel was found
            return null;
        }


    }
}