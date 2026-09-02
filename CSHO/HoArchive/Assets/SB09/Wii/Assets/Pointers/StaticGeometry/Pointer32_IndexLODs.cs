using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_IndexLODs : Pointer32{
        public List<IndexLOD> indexLODs {get;set;}

        public Pointer32_IndexLODs(List<IndexLOD> indexLODs){
            this.indexLODs = indexLODs;
        }

        public Pointer32_IndexLODs(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            file.Jump(_p);
            indexLODs = new();
            for(int i=0; i<count; i++){
                indexLODs.Add(new IndexLOD(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(IndexLOD indexLOD in indexLODs){
                indexLOD.Save(file);
            }
        }
    }
}