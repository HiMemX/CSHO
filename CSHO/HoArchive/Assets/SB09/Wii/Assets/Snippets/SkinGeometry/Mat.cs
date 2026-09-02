using HoArchive;

namespace SB09WiiAsset{
    public class Mat{
        public ushort unknown {get; set;}

        public Mat(MemoryStreamEndian file){
            unknown = file.ReadUInt16();
        }   

        public void Save(MemoryStreamEndian file){
            file.WriteE(unknown);
        }
    }
}