using System.Collections.Generic;
using HoArchive;

namespace SB09WiiAsset{
    public class Pointer32_float3s : Pointer32{
        public List<float3> coefficients {get;set;}

        public Pointer32_float3s() : base(){
            coefficients = new();
        }

        public Pointer32_float3s(List<float3> coefficients){
            this.coefficients = coefficients;
        }

        public Pointer32_float3s(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            coefficients = ReadItems<float3>(file, count, stream => new float3(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            if(_p == 0){return;}

            SaveItems(file, coefficients, (stream, item) => item.Save(stream));
        }
    }
}