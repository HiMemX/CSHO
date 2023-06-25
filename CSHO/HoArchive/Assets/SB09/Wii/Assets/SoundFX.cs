using System.ComponentModel;

namespace SB09WiiAsset{
    public class SoundFX : xBaseAsset{
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 pos {get; set;} // 0x10
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong attach {get; set;} // 0x20
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SoundBankSource source {get; set;}
        public bool needUpdate {get; set;}

        public SoundFX(HoArchive.MemoryStreamEndian file) : base(file){
            pos = new HoArchive.float3(file);
            file.Position = 0x20;
            attach = file.ReadUInt64E();
            source = new SoundBankSource(file);
            needUpdate = file.ReadBool();
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            source.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            pos.Save(file);
            file.Pad(0x04, 0x00);
            file.WriteE(attach);
            source.Save(file);
            file.WriteE(needUpdate);
            file.Pad(0x03, 0x00);
            source.SaveHeap(file);
        }
    }
}