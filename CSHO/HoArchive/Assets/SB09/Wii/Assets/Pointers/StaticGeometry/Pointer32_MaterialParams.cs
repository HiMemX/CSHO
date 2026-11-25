using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_MaterialParams : Pointer32{
        public List<MaterialParam> materialParams {get;set;} = new();

        public Pointer32_MaterialParams(){
        }

        public Pointer32_MaterialParams(List<MaterialParam> materialParams){
            this.materialParams = materialParams;
        }

        public Pointer32_MaterialParams(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            materialParams = ReadItems<MaterialParam>(file, count, stream => new MaterialParam(stream));
        }

        public new void Update(){
            foreach(MaterialParam param in materialParams){
                param.Update();
            }
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, materialParams, (stream, item) => item.Save(stream));
        }
    }
}