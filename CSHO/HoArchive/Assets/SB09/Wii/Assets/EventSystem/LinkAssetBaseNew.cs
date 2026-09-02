using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class LinkAssetBaseNew
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public __dstEvent__ srcEvent { get; set; } // I don't know if they're different from eachother so I'll make them the same for now
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public __dstEvent__ dstEvent { get; set; }
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong dstAssetID { get; set; }
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong chkAssetID { get; set; }
        public bool chkSourceParams { get; set; } // 0x20
        public bool disabled { get; set; } // 0x21
        public uint chkSourceMask { get; set; } // 0x24

        public LinkAssetBaseNew()
        {
            srcEvent = new __dstEvent__();
            dstEvent = new __dstEvent__();
        }

        public LinkAssetBaseNew(HoArchive.MemoryStreamEndian file, int nextptr)
        {
            file.Position += 8;
            dstEvent = new __dstEvent__(file, nextptr);
            file.Position -= 0x10;

            srcEvent = new __dstEvent__(file, (int)dstEvent.v);
            file.Position += 8;


            dstAssetID = file.ReadUInt64E();
            chkAssetID = file.ReadUInt64E();
            chkSourceParams = file.ReadBool();
            disabled = file.ReadBool();
            file.Align(0x04);
            chkSourceMask = file.ReadUInt32E();
        }

        public void Update()
        {
            srcEvent.Update();
            dstEvent.Update();
        }

        public void Save(HoArchive.MemoryStreamEndian file)
        {
            srcEvent.Save(file);
            dstEvent.Save(file);
            file.WriteE(dstAssetID);
            file.WriteE(chkAssetID);
            file.WriteE(chkSourceParams);
            file.WriteE(disabled);
            file.Align(0x04);
            file.WriteE(chkSourceMask);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file)
        {
            srcEvent.SaveHeap(file);
            dstEvent.SaveHeap(file);
        }
    }
}