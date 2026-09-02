
namespace SB09WiiAsset{
    public class Feature{
        public ushort passIndex {get; set;}
        public byte passCount {get; set;}
        public byte pad {get; set;}
        public uint featureFlags {get; set;}

        public Feature(){}

        public Feature(HoArchive.MemoryStreamEndian file){
            passIndex = file.ReadUInt16E();
            passCount = file.ReadByte();
            pad = file.ReadByte();
            featureFlags = file.ReadUInt32E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(passIndex);
            file.WriteE(passCount);
            file.WriteE(pad);
            file.WriteE(featureFlags);
        }

    }
}