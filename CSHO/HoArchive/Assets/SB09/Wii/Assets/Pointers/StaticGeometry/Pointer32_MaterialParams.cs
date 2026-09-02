using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_MaterialParams : Pointer32{
        public List<MaterialParam> materialParams {get;set;}

        public Pointer32_MaterialParams(){
        }

        public Pointer32_MaterialParams(List<MaterialParam> materialParams){
            this.materialParams = materialParams;
        }

        public Pointer32_MaterialParams(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            file.Jump(_p);
            materialParams = new();
            for(int i=0; i<count; i++){
                materialParams.Add(new MaterialParam(file));
            }

            file.Return();
        }

        public new void Update(){
            foreach(MaterialParam param in materialParams){
                param.Update();
            }
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(MaterialParam param in materialParams){
                param.Save(file);
            }
        }
    }
}