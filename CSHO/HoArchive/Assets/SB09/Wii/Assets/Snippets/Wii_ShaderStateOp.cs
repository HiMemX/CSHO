using System.ComponentModel;

namespace SB09WiiAsset{
    public class Wii_ShaderStateOp : StateOp{
        public uint flags {get; set;}
        public byte matSource {get; set;}
        public byte channelCount {get; set;}
        public byte streamCount {get; set;}
        public byte uvMapping {get; set;}      

        public Wii_ShaderStateOp(HoArchive.MemoryStreamEndian file) : base(file){
            flags = file.ReadUInt32E();
            matSource = file.ReadByte();
            channelCount = file.ReadByte();
            streamCount = file.ReadByte();
            uvMapping = file.ReadByte();
        }

        public new void Update(){
            size = 0x0C;
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(flags);
            file.WriteE(matSource);
            file.WriteE(channelCount);
            file.WriteE(streamCount);
            file.WriteE(uvMapping);
        }
    }
}