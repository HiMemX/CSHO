namespace SB09WiiAsset{
    public class ViewAttrib{
        public byte spaceFilter {get;set;}

        public ViewAttrib(HoArchive.MemoryStreamEndian file){
            spaceFilter = file.ReadByte();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(spaceFilter);
        }
    }
}