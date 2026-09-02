
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class PlatformType{ // Union style
        public enPlatformType type; // Only supposed to be mod by program
        public bool use_banking {get; set;}
        public ushort MotionFlags {get;set;}
        public int pad {get;set;}

        public PlatformType(){}

        public PlatformType(MemoryStreamEndian file){
            use_banking = file.ReadBool();
            MotionFlags = file.ReadUInt16E();
            pad = file.ReadInt32E();

        }

        public virtual void Save(MemoryStreamEndian file){
            file.WriteE(use_banking);
            file.WriteE(MotionFlags);
            file.WriteE(pad);

            file.Pad(0x14, 0);
            file.WriteE(true);
        
            // Default values for some reason
            file.Pad(0x1B, 0);
            file.WriteE(1.0f);
            file.WriteE(1.0f);
            file.Position -= 0x38;
        }

    }
}