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
            file.Jump(_p);
            matList = new List<Mat>();
            for(int x=0; x<count; x++){
                matList.Add(new Mat(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(Mat mat in matList){
                mat.Save(file);
            }
        }
    }
}