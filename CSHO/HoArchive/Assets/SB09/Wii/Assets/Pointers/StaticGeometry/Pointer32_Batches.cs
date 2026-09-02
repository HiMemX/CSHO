using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_BatchInfos : Pointer32{
        public List<BatchInfo> batchInfos {get;set;}

        public Pointer32_BatchInfos(List<BatchInfo> batchInfos){
            this.batchInfos = batchInfos;
        }

        public Pointer32_BatchInfos(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            file.Jump(_p);
            batchInfos = new();
            for(int i=0; i<count; i++){
                batchInfos.Add(new BatchInfo(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            if(_p == 0){return;}

            base.Save(file);
            foreach(BatchInfo batch in batchInfos){
                batch.Save(file);
            }
        }
    }
}