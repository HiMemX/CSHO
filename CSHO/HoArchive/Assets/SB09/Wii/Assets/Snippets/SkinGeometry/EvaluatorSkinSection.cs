using HoArchive;

namespace SB09WiiAsset{
    public class EvaluatorSkinSection{
        public ushort start {get;set;} = new();
        public byte size {get;set;} = new();
        public byte pad {get;set;} = new();

        public EvaluatorSkinSection(){}

        public EvaluatorSkinSection(MemoryStreamEndian file){
            start = file.ReadUInt16E();
            size = file.ReadByte();
            pad = file.ReadByte();
        }

        public void Save(MemoryStreamEndian file){
            file.WriteE(start);
            file.WriteE(size);
            file.WriteE(pad);
        }
    }
}