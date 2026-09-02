using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class SplineType : PlatformType{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong StartingCurve {get;set;}
        public float Speed {get;set;}
        public float lean_modifier {get;set;}

        public SplineType() : base(){
            type = enPlatformType.SPLINE;
        }

        public SplineType(MemoryStreamEndian file) : base(file){
            type = enPlatformType.SPLINE;

            StartingCurve = file.ReadUInt64E();
            Speed = file.ReadFloat32E();
            lean_modifier = file.ReadFloat32E();
        }

        public override void Save(MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(StartingCurve);
            file.WriteE(Speed);
            file.WriteE(lean_modifier);
        }
    }
}