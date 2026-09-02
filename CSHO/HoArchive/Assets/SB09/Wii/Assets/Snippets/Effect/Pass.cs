namespace SB09WiiAsset{
    public class Pass{
        public ushort shaderIndex {get; set;}

        public Pass(){}

        public Pass(HoArchive.MemoryStreamEndian file){
            shaderIndex = file.ReadUInt16E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(shaderIndex);
        }

    }
}