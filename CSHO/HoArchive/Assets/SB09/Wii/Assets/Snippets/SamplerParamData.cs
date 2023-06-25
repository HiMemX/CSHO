using System.ComponentModel;

namespace SB09WiiAsset{
    public class SamplerParamData{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong textureID {get; set;}
        public uint wad {get; set;}
        public float mipmaplodbias {get; set;}

        public SamplerParamData(){
            
        }

        public SamplerParamData(HoArchive.MemoryStreamEndian file){
            textureID = file.ReadUInt64E();
            wad = file.ReadUInt32E();
            mipmaplodbias = file.ReadFloat32E();
        }

        public void Update(HoArchive.TOCEntry entry){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(textureID);
            file.WriteE(wad);
            file.WriteE(mipmaplodbias);
        }
    }
}