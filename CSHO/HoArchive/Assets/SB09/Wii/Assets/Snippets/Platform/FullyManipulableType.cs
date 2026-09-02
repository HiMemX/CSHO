using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class FullyManipulableType : PlatformType{

        public FullyManipulableType() : base(){
            type = enPlatformType.FULLY_MANIPULABLE;
        }

        public FullyManipulableType(MemoryStreamEndian file) : base(file){
            type = enPlatformType.FULLY_MANIPULABLE;
        }

        public override void Save(MemoryStreamEndian file){
            base.Save(file);
            
        }
    }
}