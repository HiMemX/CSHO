using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_CollKDTree : Pointer32{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CollKDTree collKDTree {get;set;}

        public Pointer32_CollKDTree(CollKDTree collKDTree){
            this.collKDTree = collKDTree;
        }

        public Pointer32_CollKDTree(HoArchive.MemoryStreamEndian file) : base(file){
            if(_p == 0){
                collKDTree = new CollKDTree();
                return;
            }

            file.Jump(_p);
            collKDTree = new CollKDTree(file);
            file.Return();
        }

        public new void Update(){
            collKDTree.Update();
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            if(_p == 0){
                return;
            }

            base.Save(file);
            collKDTree.Save(file);
        }
    }
}