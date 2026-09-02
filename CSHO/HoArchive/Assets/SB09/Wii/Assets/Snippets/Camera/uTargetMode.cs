using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class uTargetMode{
        [TypeConverter(typeof(Point3Converter))]
        public HoArchive.float3 rotation {get; set;} // Not like this in Dwarf, 
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong target {get; set;}

        public uTargetMode(HoArchive.MemoryStreamEndian file){
            file.Jump((uint)file.Position);
            rotation = new float3(file);
            file.Return();
            target = file.ReadUInt64E();

            file.ReadUInt64E(); // "Padding"
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file, TargetMode targetMode){
            if(targetMode == TargetMode.Rotation){rotation.Save(file);}
            if(targetMode == TargetMode.Target){file.WriteE(target);}
            file.Align(0x10);
        }

    }
}