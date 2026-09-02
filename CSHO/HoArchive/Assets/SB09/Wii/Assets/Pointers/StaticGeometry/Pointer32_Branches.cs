using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Branches : Pointer32{
        public List<Branch> branches {get;set;}

        public Pointer32_Branches(List<Branch> branches){
            this.branches = branches;
        }

        public Pointer32_Branches(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            file.Jump(_p);
            branches = new();
            for(int i=0; i<count; i++){
                branches.Add(new Branch(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(Branch branch in branches){
                branch.Save(file);
            }
        }
    }
}