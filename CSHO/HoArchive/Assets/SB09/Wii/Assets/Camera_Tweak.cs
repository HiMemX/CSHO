using System.ComponentModel;

namespace SB09WiiAsset{
    public class Camera_Tweak : xBaseAsset{
        public int priority {get;set;} = 0;
        public float time {get;set;} = 0;
        public float pitch_adjust {get;set;} = 0;
        public float dist_adjust {get;set;} = 0;
        public float pivot_height_adjust {get;set;} = 0;
        public float fov_adjust {get;set;} = 0;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public dirTweak dir_adjust {get;set;} = new();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get;set;} = new();


        public Camera_Tweak() : base(){}
        
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