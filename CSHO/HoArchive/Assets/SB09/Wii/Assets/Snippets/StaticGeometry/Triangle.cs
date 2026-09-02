namespace SB09WiiAsset{
    public class Triangle{
        public uint v0 {get;set;} // This is a pointer to the vertex rawblob
        public int v1flags {get;set;}
        public int v2flags {get;set;}
        public uint offset {get;set;}

        public Triangle(){}

        public Triangle(HoArchive.MemoryStreamEndian file){
            v0 = file.ReadUInt32E();
            v1flags = file.ReadInt32E();
            v2flags = file.ReadInt32E();
            offset = file.ReadUInt32E();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(v0);
            file.WriteE(v1flags);
            file.WriteE(v2flags);
            file.WriteE(offset);
        }
    }
}