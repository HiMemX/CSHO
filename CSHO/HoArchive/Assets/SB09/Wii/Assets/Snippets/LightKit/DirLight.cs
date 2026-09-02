using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class DirLight{
        [TypeConverter(typeof(FloatColorRGBConverter))]
        public FloatColorRGB color {get; set;}
        [TypeConverter(typeof(Point3Converter))]
        public HoArchive.float3 orientation {get; set;}

        public DirLight(MemoryStreamEndian file){
            color = new FloatColorRGB(file);
            orientation = new float3(file);
        }

        public void Save(MemoryStreamEndian file){
            color.Save(file);
            orientation.Save(file);
        }
    }
}