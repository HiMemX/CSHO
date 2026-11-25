using HoArchive;

namespace SB09WiiAsset{
    public class Mat{
        public ushort unknown {get; set;} = new();

        public Mat(){}

        public Mat(MemoryStreamEndian file){
            unknown = file.ReadUInt16();
        }   

        public void Save(MemoryStreamEndian file){
            file.WriteE(unknown);
        }
    }
}