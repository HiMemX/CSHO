using System.Collections.Generic;
using HoArchive;

namespace SB09WiiAsset{
    public class Pointer32_float3s : Pointer32{
        public List<float3> coefficients {get;set;}

        public Pointer32_float3s(List<float3> coefficients){
            this.coefficients = coefficients;
        }

        public Pointer32_float3s(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            file.Jump(_p);
            coefficients = new();
            for(int i=0; i<count; i++){
                coefficients.Add(new float3(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            if(_p == 0){return;}

            base.Save(file);
            foreach(float3 batch in coefficients){
                batch.Save(file);
            }
        }
    }
}