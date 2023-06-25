using System.ComponentModel;

namespace SB09WiiAsset{
    public class BSP : xEntAsset{
        public bool AlwaysVisible {get; set;}
        public bool AlwaysCollide {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong ReferenceGroup {get; set;}
        public LinkAsset EventLinksNew;


        public BSP(HoArchive.MemoryStreamEndian file) : base(file){
            AlwaysVisible = file.ReadBool();
            AlwaysCollide = file.ReadBool();
            file.Position = 0x100;
            ReferenceGroup = file.ReadUInt64E();
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            Pos = new HoArchive.float3(0, 0, 0);
            Orientation = new HoArchive.float3(0, 0, 0);
            Scale = new HoArchive.float3(1, 1, 1);
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(AlwaysVisible);
            file.WriteE(AlwaysCollide);
            file.PadTo(0x100, 0x00);
            file.WriteE(ReferenceGroup);
            EventLinksNew.Save(file);

            base.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}