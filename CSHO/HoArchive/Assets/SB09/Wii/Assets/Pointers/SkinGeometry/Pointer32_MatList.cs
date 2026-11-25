using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_MatList : Pointer32{
        public List<Mat> matList {get; set;}

        public Pointer32_MatList(){
            matList = new List<Mat>();
        }

        public Pointer32_MatList(List<Mat> matList){
            this.matList = matList;
        }

        public Pointer32_MatList(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            matList = ReadItems<Mat>(file, count, stream => new Mat(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, matList, (stream, item) => item.Save(stream));
        }
    }
}