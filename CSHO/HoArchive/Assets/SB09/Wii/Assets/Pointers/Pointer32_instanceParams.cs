using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_instanceParams : Pointer32{
        public List<instanceParam> instanceParams {get; set;}

        public Pointer32_instanceParams() : base(){
            instanceParams = new();
        }

        public Pointer32_instanceParams(HoArchive.MemoryStreamEndian file, uint instanceParamCount) : base(file){
            instanceParams = ReadItems<instanceParam>(file, instanceParamCount, stream => new instanceParam(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, instanceParams, (stream, item) => item.Save(stream));
        }
    }
}