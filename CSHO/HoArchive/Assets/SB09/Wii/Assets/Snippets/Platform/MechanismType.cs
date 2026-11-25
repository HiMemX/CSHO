using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class MechanismType : PlatformType{
        public byte MechType {get;set;} = new(); // Maybe convert to enum later
        public byte flags {get;set;} = new();
        public byte SlideAxis {get;set;} = new();
        public byte RotationAxis {get;set;} = new();
        public byte ScaleAxis {get;set;} = new();
        public float SlideDistance {get;set;} = new();
        public float SlideDuration {get;set;} = new();
        public float SlideAccelTime {get;set;} = new();
        public float SlideDecelTime {get;set;} = new();
        public float RotationDistance {get;set;} = new();
        public float RotationDuration {get;set;} = new();
        public float RotationAccelTime {get;set;} = new();
        public float RotationDecelTime {get;set;} = new();
        public float ReturnDelay {get;set;} = new();
        public float EndCycleDelay {get;set;} = new();
        public float ScaleAmount {get;set;} = new();
        public float ScaleDuration {get;set;} = new();

        public MechanismType() : base(){
            type = enPlatformType.MECHANISM;
        }

        public MechanismType(MemoryStreamEndian file) : base(file){
            type = enPlatformType.MECHANISM;
            
            MechType = file.ReadByte();
            flags = file.ReadByte();
            SlideAxis = file.ReadByte();
            RotationAxis = file.ReadByte();
            ScaleAxis = file.ReadByte();
            file.ReadBytes(3);
            SlideDistance = file.ReadFloat32E();
            SlideDuration = file.ReadFloat32E();
            SlideAccelTime = file.ReadFloat32E();
            SlideDecelTime = file.ReadFloat32E();
            RotationDistance = file.ReadFloat32E();
            RotationDuration = file.ReadFloat32E();
            RotationAccelTime = file.ReadFloat32E();
            RotationDecelTime = file.ReadFloat32E();
            ReturnDelay = file.ReadFloat32E();
            EndCycleDelay = file.ReadFloat32E();
            ScaleAmount = file.ReadFloat32E();
            ScaleDuration = file.ReadFloat32E();
        }

        public override void Save(MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(MechType);
            file.WriteE(flags);
            file.WriteE(SlideAxis);
            file.WriteE(RotationAxis);
            file.WriteE(ScaleAxis);
            file.Pad(3, 0);
            file.WriteE(SlideDistance);
            file.WriteE(SlideDuration);
            file.WriteE(SlideAccelTime);
            file.WriteE(SlideDecelTime);
            file.WriteE(RotationDistance);
            file.WriteE(RotationDuration);
            file.WriteE(RotationAccelTime);
            file.WriteE(RotationDecelTime);
            file.WriteE(ReturnDelay);
            file.WriteE(EndCycleDelay);
            file.WriteE(ScaleAmount);
            file.WriteE(ScaleDuration);
        }
    }
}
