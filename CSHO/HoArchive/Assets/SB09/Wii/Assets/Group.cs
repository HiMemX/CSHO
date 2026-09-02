using System.Collections.Generic;
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class Group : xBaseAsset{
        public ushort itemCount { get; set; }
        public short maxDynamicSize { get; set; }
        public ushort flags { get; set; }
        // Pad 2 bytes
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew { get; set; }
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong firstItemMarker { get; set; }

        public AssetIDList uids { get; set; }

        public Group(HoArchive.MemoryStreamEndian file) : base(file)
        {
            itemCount = file.ReadUInt16E();
            maxDynamicSize = file.ReadInt16E();
            flags = file.ReadUInt16E();
            file.Align(0x08);
            EventLinksNew = new LinkAsset(file);
            firstItemMarker = file.ReadUInt64E();

            uids = new AssetIDList(file, (uint)itemCount);
        }

        public override void Update(HoArchive.TOCEntry entry)
        {
            base.Update(entry);
            itemCount = (ushort)uids.Count;
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);
            file.WriteE(itemCount);
            file.WriteE(maxDynamicSize);
            file.WriteE(flags);
            file.Pad(0x02, 0);

            EventLinksNew.Save(file);

            file.WriteE(firstItemMarker);
            uids.Save(file);

            EventLinksNew.SaveHeap(file);

        }
    }
}