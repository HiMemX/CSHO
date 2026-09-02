using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class MovepointType : PlatformType{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong StartingMovePoint {get;set;}
        public uint flags {get;set;}
        public float Speed {get;set;}

        public MovepointType() : base(){
            type = enPlatformType.MOVEPOINT;
        }

        public MovepointType(MemoryStreamEndian file) : base(file){
            type = enPlatformType.MOVEPOINT;
            
            StartingMovePoint = file.ReadUInt64E();
            flags = file.ReadUInt32E();
            Speed = file.ReadFloat32E();
        }

        public override void Save(MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(StartingMovePoint);
            file.WriteE(flags);
            file.WriteE(Speed);
        }
    }
}