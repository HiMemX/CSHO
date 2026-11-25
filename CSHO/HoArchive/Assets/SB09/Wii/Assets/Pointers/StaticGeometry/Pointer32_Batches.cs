using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_BatchInfos : Pointer32{
        public List<BatchInfo> batchInfos {get;set;}

        public Pointer32_BatchInfos() : base(){
            batchInfos = new();
        }

        public Pointer32_BatchInfos(List<BatchInfo> batchInfos){
            this.batchInfos = batchInfos;
        }

        public Pointer32_BatchInfos(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            batchInfos = ReadItems<BatchInfo>(file, count, stream => new BatchInfo(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            if(_p == 0){return;}

            SaveItems(file, batchInfos, (stream, item) => item.Save(stream));
        }
    }
}