using System.ComponentModel;

namespace SB09WiiAsset{
    public class Camera_Tweak : xBaseAsset{
        public int priority {get;set;}
        public float time {get;set;}
        public float pitch_adjust {get;set;}
        public float dist_adjust {get;set;}
        public float pivot_height_adjust {get;set;}
        public float fov_adjust {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public dirTweak dir_adjust {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get;set;}

        
        public Camera_Tweak(HoArchive.MemoryStreamEndian file) :base(file){
            priority = file.ReadInt32E();
            time = file.ReadFloat32E();
            pitch_adjust = file.ReadFloat32E();
            dist_adjust = file.ReadFloat32E();
            pivot_height_adjust = file.ReadFloat32E();
            fov_adjust = file.ReadFloat32E();
            dir_adjust = new(file);
            file.Align(0x08);
            EventLinksNew = new(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(priority);
            file.WriteE(time);
            file.WriteE(pitch_adjust);
            file.WriteE(dist_adjust);
            file.WriteE(pivot_height_adjust);
            file.WriteE(fov_adjust);
            dir_adjust.Save(file);
            file.Align(0x08);
            EventLinksNew.Save(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}