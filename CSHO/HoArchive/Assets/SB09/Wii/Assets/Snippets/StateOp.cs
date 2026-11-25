using System.ComponentModel;

namespace SB09WiiAsset{
    public class StateOp{
        [TypeConverter(typeof(StateOpTypeConverter))]
        public StateOpTypeEnum type {get; set;} = new();
        public byte pad {get; set;} = new();
        public ushort size {get; set;} = new();

        public StateOp(){}

        public StateOp(HoArchive.MemoryStreamEndian file){
            type = (StateOpTypeEnum)file.ReadByte();
            pad = file.ReadByte();
            size = file.ReadUInt16E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE((byte)type);
            file.WriteE(pad);
            file.WriteE(size);
        }
    }
}


