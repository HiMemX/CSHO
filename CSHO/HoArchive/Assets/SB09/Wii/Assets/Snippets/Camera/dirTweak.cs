using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class dirTweak{
        public float yaw {get;set;}
        public float pitch {get;set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong target {get;set;}
        public float yaw_range {get;set;}
        public float pitch_range {get;set;}
        public float rigidity {get;set;}

        public dirTweak(HoArchive.MemoryStreamEndian file){
            yaw = file.ReadFloat32E();
            pitch = file.ReadFloat32E();
            target = file.ReadUInt64E();
            yaw_range = file.ReadFloat32E();
            pitch_range = file.ReadFloat32E();
            rigidity = file.ReadFloat32E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(yaw);
            file.WriteE(pitch);
            file.WriteE(target);
            file.WriteE(yaw_range);
            file.WriteE(pitch_range);
            file.WriteE(rigidity);
        }
    }
}