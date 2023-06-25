using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_SamplerParamData : Pointer32{
        public List<SamplerParamData> samp {get; set;}

        public Pointer32_SamplerParamData(){
            samp = new List<SamplerParamData>();
        }

        public Pointer32_SamplerParamData(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            file.Jump(_p);
            samp = new List<SamplerParamData>();
            for(int x=0; x<count; x++){
                samp.Add(new SamplerParamData(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(SamplerParamData flt in samp){
                flt.Save(file);
            }
        }
    }
}