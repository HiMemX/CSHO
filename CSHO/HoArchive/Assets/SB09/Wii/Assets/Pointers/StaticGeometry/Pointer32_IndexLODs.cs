using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_IndexLODs : Pointer32{
        public List<IndexLOD> indexLODs {get;set;}

        public Pointer32_IndexLODs() : base(){
            indexLODs = new();
        }

        public Pointer32_IndexLODs(List<IndexLOD> indexLODs){
            this.indexLODs = indexLODs;
        }

        public Pointer32_IndexLODs(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            indexLODs = ReadItems<IndexLOD>(file, count, stream => new IndexLOD(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, indexLODs, (stream, item) => item.Save(stream));
        }
    }
}