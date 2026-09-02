using System.ComponentModel;

namespace SB09WiiAsset{
    public class Trampoline : xEntAsset{
        
        public float MinBounce { get; set; }
        public float MaxBounce { get; set; }
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;}

        public Trampoline(HoArchive.MemoryStreamEndian file) : base(file)
        {
            MinBounce = file.ReadFloat32E();
            MaxBounce = file.ReadFloat32E();
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry)
        {
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);
            file.WriteE(MinBounce);
            file.WriteE(MaxBounce);
            EventLinksNew.Save(file);

            base.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}