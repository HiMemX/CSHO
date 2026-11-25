using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class TeeterType : PlatformType{
        public float InitialTilt {get;set;} = new();
        public float MaxTilt {get;set;} = new();
        public float MaxIdleTilt {get;set;} = new();
        public float Speed {get;set;} = new();
        public float SpeedIdle {get;set;} = new();
        public bool TiltBack {get;set;} = new();

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