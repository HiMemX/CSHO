using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class ConveyorBeltType : PlatformType{
        public float BeltSpeed {get;set;}
        public uint MotionAxis {get;set;}

        public ConveyorBeltType() : base(){
            type = enPlatformType.CONVEYOR_BELT;
        }

        public ConveyorBeltType(MemoryStreamEndian file) : base(file){
            type = enPlatformType.CONVEYOR_BELT;
            BeltSpeed = file.ReadFloat32E();
            MotionAxis = file.ReadUInt32E();
        }

        public override void Save(MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(BeltSpeed);
            file.WriteE(MotionAxis);
        }
    }
}