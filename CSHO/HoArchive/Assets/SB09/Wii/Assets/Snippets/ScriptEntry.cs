using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class ScriptEntry
    {
        public float delay { get; set; } = new();
        public uint unknown0 { get; set; } = new();
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong dstAsset { get; set; } = new();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public __dstEvent__ dstEvent { get; set; }
        public bool unknown1 { get; set; } = new();
        public byte unknown2 { get; set; } = new();
        public byte unknown3 { get; set; } = new();
        public byte unknown4 { get; set; } = new();
        public uint unknown5 { get; set; } = new();

        public ScriptEntry()
        {
            dstEvent = new();
        }

        public ScriptEntry(HoArchive.MemoryStreamEndian file, int nextptr)
        {
            delay = file.ReadFloat32E();
            unknown0 = file.ReadUInt32E();
            dstAsset = file.ReadUInt64E();
            dstEvent = new(file, nextptr);
            unknown1 = file.ReadBool();
            unknown2 = file.ReadByte();
            unknown3 = file.ReadByte();
            unknown4 = file.ReadByte();
            unknown5 = file.ReadUInt32E();
        }

        public void Update()
        {
            dstEvent.Update();
        }

        public void Save(HoArchive.MemoryStreamEndian file)
        {
            file.WriteE(delay);
            file.WriteE(unknown0);
            file.WriteE(dstAsset);
            dstEvent.Save(file);
            file.WriteE(unknown1);
            file.WriteE(unknown2);
            file.WriteE(unknown3);
            file.WriteE(unknown4);
            file.WriteE(unknown5);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file)
        {
            dstEvent.SaveHeap(file);
        }
    }
}