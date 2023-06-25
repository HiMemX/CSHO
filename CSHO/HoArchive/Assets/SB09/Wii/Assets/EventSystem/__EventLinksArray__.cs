namespace SB09WiiAsset{
    public class __EventLinksArray__{
        public uint count;
        public Pointer32_rawdata data;

        public __EventLinksArray__(HoArchive.MemoryStreamEndian file){
            count = file.ReadUInt32E();
            data = new Pointer32_rawdata(file, count);
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(count);
            data.SavePointer(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            data.Save(file);
        }
    }
}