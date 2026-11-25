using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_SamplerParamData : Pointer32{
        public List<SamplerParamData> samp {get; set;}

        public Pointer32_SamplerParamData(){
            samp = new List<SamplerParamData>();
        }

        public Pointer32_SamplerParamData(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            samp = ReadItems<SamplerParamData>(file, count, stream => new SamplerParamData(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, samp, (stream, item) => item.Save(stream));
        }
    }
}