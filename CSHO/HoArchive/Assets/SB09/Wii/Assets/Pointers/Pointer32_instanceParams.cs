using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_instanceParams : Pointer32{
        public List<instanceParam> instanceParams {get; set;}

        public Pointer32_instanceParams(HoArchive.MemoryStreamEndian file, uint instanceParamCount) : base(file){
            instanceParams = new List<instanceParam>();
            file.Jump(_p);
            for(int i=0; i<instanceParamCount; i++){
                instanceParams.Add(new instanceParam(file));
            }
            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(instanceParam param in instanceParams){
                param.Save(file);
            }
        }
    }
}