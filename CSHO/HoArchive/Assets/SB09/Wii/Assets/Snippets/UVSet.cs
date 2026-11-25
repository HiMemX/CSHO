namespace SB09WiiAsset{
    public class UVSet{
        public float U {get; set;} = new();
        public float V {get; set;} = new();

        public UVSet(){}

        public UVSet(HoArchive.MemoryStreamEndian file){
            U = file.ReadFloat32E();
            V = file.ReadFloat32E();
        }


        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(U);
            file.WriteE(V);
        }
    }
}