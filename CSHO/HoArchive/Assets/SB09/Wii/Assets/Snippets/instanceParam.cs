namespace SB09WiiAsset{
    public class instanceParam{
        public uint unknown0 {get; set;}
        public uint unknown1 {get; set;}
        public float unknown2 {get; set;}
        public float unknown3 {get; set;}
        public float unknown4 {get; set;}
        public float unknown5 {get; set;}

        public instanceParam(){}

        public instanceParam(HoArchive.MemoryStreamEndian file){
            unknown0 = file.ReadUInt32E();
            unknown1 = file.ReadUInt32E();
            unknown2 = file.ReadFloat32E();
            unknown3 = file.ReadFloat32E();
            unknown4 = file.ReadFloat32E();
            unknown5 = file.ReadFloat32E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(unknown0);
            file.WriteE(unknown1);
            file.WriteE(unknown2);
            file.WriteE(unknown3);
            file.WriteE(unknown4);
            file.WriteE(unknown5);
        }
    }
}