using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset
{
    public class Wii_ShaderStateOp : StateOp
    {

        [TypeConverter(typeof(UInt32BinaryStringConverter))]
        public uint flags { get; set; } = new();
        public byte matSource { get; set; } = new();
        public byte channelCount { get; set; } = new();
        public byte streamCount { get; set; } = new();
        public byte uvMapping { get; set; } = new();

        public Wii_ShaderStateOp() : base()
        {
            size = 0x0C;
        }

        public Wii_ShaderStateOp(HoArchive.MemoryStreamEndian file) : base(file)
        {
            flags = file.ReadUInt32E();
            matSource = file.ReadByte();
            channelCount = file.ReadByte();
            streamCount = file.ReadByte();
            uvMapping = file.ReadByte();
        }

        public new void Update()
        {
            size = 0x0C;
        }

        public new void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);
            file.WriteE(flags);
            file.WriteE(matSource);
            file.WriteE(channelCount);
            file.WriteE(streamCount);
            file.WriteE(uvMapping);
        }
    }
}