using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class TeeterType : PlatformType{
        public float InitialTilt {get;set;}
        public float MaxTilt {get;set;}
        public float MaxIdleTilt {get;set;}
        public float Speed {get;set;}
        public float SpeedIdle {get;set;}
        public bool TiltBack {get;set;}

        public TeeterType() : base(){
            type = enPlatformType.TEETER;
        }

        public TeeterType(MemoryStreamEndian file) : base(file){
            type = enPlatformType.TEETER;
            
            InitialTilt = file.ReadFloat32E();
            MaxTilt = file.ReadFloat32E();
            MaxIdleTilt = file.ReadFloat32E();
            Speed = file.ReadFloat32E();
            SpeedIdle = file.ReadFloat32E();
            TiltBack = file.ReadBool();
        }

        public override void Save(MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(InitialTilt);
            file.WriteE(MaxTilt);
            file.WriteE(MaxIdleTilt);
            file.WriteE(Speed);
            file.WriteE(SpeedIdle);
            file.WriteE(TiltBack);
        }
    }
}