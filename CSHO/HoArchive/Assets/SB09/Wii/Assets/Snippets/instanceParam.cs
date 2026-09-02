namespace SB09WiiAsset{
    public class instanceParam{
        public instanceParamHandle handle {get; set;}
        public float unknown2 {get; set;}
        public float unknown3 {get; set;}
        public float unknown4 {get; set;}
        public float unknown5 {get; set;}

        public instanceParam(){}

        public instanceParam(HoArchive.MemoryStreamEndian file){
            handle = new instanceParamHandle(file);
            unknown2 = file.ReadFloat32E();
            unknown3 = file.ReadFloat32E();
            unknown4 = file.ReadFloat32E();
            unknown5 = file.ReadFloat32E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            handle.Save(file);
            file.WriteE(unknown2);
            file.WriteE(unknown3);
            file.WriteE(unknown4);
            file.WriteE(unknown5);
        }
    }
}