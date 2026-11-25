namespace SB09WiiAsset{
    public class Branch{
        public uint leftInfo {get;set;} = new();
        public uint rightInfo {get;set;} = new();
        public float leftValue {get;set;} = new();
        public float rightValue {get;set;} = new();

        public Branch(){}

        public Branch(HoArchive.MemoryStreamEndian file){
            leftInfo = file.ReadUInt32E();
            rightInfo = file.ReadUInt32E();
            leftValue = file.ReadFloat32E();
            rightValue = file.ReadFloat32E();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(leftInfo);
            file.WriteE(rightInfo);
            file.WriteE(leftValue);
            file.WriteE(rightValue);
        }
    }
}