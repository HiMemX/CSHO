using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Branches : Pointer32{
        public List<Branch> branches {get;set;}

        public Pointer32_Branches() : base(){
            branches = new();
        }

        public Pointer32_Branches(List<Branch> branches){
            this.branches = branches;
        }

        public Pointer32_Branches(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            branches = ReadItems<Branch>(file, count, stream => new Branch(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, branches, (stream, item) => item.Save(stream));
        }
    }
}