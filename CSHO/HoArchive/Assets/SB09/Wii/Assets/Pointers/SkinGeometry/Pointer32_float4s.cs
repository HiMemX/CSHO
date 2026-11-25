using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_float4s : Pointer32{
        public List<HoArchive.float4> float4s {get; set;}

        public Pointer32_float4s(){
            float4s = new List<HoArchive.float4>();
        }

        public Pointer32_float4s(List<HoArchive.float4> float4s){
            this.float4s = float4s;
        }

        public Pointer32_float4s(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            float4s = ReadItems<HoArchive.float4>(file, count, stream => new HoArchive.float4(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, float4s, (stream, item) => item.Save(stream));
        }
    }
}