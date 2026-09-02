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
            file.Jump(_p);
            float4s = new List<HoArchive.float4>();
            for(int x=0; x<count; x++){
                float4s.Add(new HoArchive.float4(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(HoArchive.float4 section in float4s){
                section.Save(file);
            }
        }
    }
}