
namespace SB09WiiAsset{
    public class LinkAsset{
        public __EventLinksArray__ EventLinksArray;

        public LinkAsset(HoArchive.MemoryStreamEndian file){
            EventLinksArray = new __EventLinksArray__(file);
        }

        public void Update(){
            EventLinksArray.Update();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            EventLinksArray.Save(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            file.PadAlign(0x08, 0x00);
            EventLinksArray.SaveHeap(file);
        }
    }
}