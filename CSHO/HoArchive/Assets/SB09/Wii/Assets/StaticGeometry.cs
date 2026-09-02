
using HoArchive;

namespace SB09WiiAsset{
    public class StaticGeometry : GeometryAsset{
        public StaticGeometry(HoArchive.MemoryStreamEndian file) : base(file){}

        public override void Update(TOCEntry entry)
        {
            base.Update(entry);
        }

        public override void Save(MemoryStreamEndian file)
        {
            base.Save(file);
            base.SaveHeap(file);
        }
    }
}