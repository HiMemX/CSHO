using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class LightKit : Asset.AssetEntity{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong lookupTex {get; set;}
        [TypeConverter(typeof(FloatColorRGBConverter))]
        public FloatColorRGB ambientColor {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DirLight_Array5 dir { get; set; }
        public uint pad {get; set;}

        public LightKit(HoArchive.MemoryStreamEndian file){
            lookupTex = file.ReadUInt64E();
            ambientColor = new FloatColorRGB(file);
            dir = new DirLight_Array5(file);
            pad = file.ReadUInt32E();
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(lookupTex);
            ambientColor.Save(file);
            dir.Save(file);
            file.WriteE(pad);
        }
    }
}