namespace SB09WiiAsset{
    public class LOD{
        public ushort featureIndex {get; set;}
        public ushort featureCount {get; set;}

        public LOD(){}

        public LOD(HoArchive.MemoryStreamEndian file){
            featureIndex = file.ReadUInt16E();
            featureCount = file.ReadUInt16E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(featureIndex);
            file.WriteE(featureCount);
        }

    }
}