using System.ComponentModel;

namespace SB09WiiAsset{
    public class Direction : xBaseAsset{
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 location {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 direction {get; set;}
        public LinkAsset EventLinksNew;

        public Direction(HoArchive.MemoryStreamEndian file) : base(file){
            location = new HoArchive.float3(file);
            direction = new HoArchive.float3(file);
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            location.Save(file);
            direction.Save(file);
            EventLinksNew.Save(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}