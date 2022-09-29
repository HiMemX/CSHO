
namespace SB09Assets{
    public class LinkAsset{
        public __EventLinksArray__ EventLinksArray;

        public LinkAsset(HoArchive.BinaryReaderEndian file, uint elementOffset, uint blobSize){
            EventLinksArray = new __EventLinksArray__(file, elementOffset, blobSize);
        }

    }
}