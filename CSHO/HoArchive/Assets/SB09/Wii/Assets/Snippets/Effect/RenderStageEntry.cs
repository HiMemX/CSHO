namespace SB09WiiAsset{
    public class RenderStageEntry{ // All this for a single byte :D
        public byte lodIndex {get; set;}

        public RenderStageEntry(){}

        public RenderStageEntry(HoArchive.MemoryStreamEndian file){
            lodIndex = file.ReadByte();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(lodIndex);
        }

    }
}