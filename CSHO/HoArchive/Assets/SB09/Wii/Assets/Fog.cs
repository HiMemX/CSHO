using System.ComponentModel;

namespace SB09WiiAsset{
    public class Fog : xBaseAsset{
        //[TypeConverter(typeof(HoArchive.RGBA8888Converter))]
        [TypeConverter(typeof(HoArchive.RGBA8888Converter))]
        public HoArchive.RGBA8888 color {get; set;} = new();
        public float start {get; set;} = 0;
        public float end {get; set;} = 100;
        [TypeConverter(typeof(SB09WiiAsset.FogInterpolationModeConverter))]
        public SB09WiiAsset.FogInterpolationMode mode {get; set;} = FogInterpolationMode.FOGMODE_Linear;
        public float heightStart {get; set;} = 0;
        public float heightEnd {get; set;} = 0;
        [TypeConverter(typeof(SB09WiiAsset.FogInterpolationModeConverter))]
        public SB09WiiAsset.FogInterpolationMode heightMode {get; set;} = FogInterpolationMode.FOGMODE_None;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;} = new();

        public Fog() : base(){}

        public Fog(HoArchive.MemoryStreamEndian file) : base(file){
            color = new HoArchive.RGBA8888(file);
            start = file.ReadFloat32E();
            end = file.ReadFloat32E();
            mode = (SB09WiiAsset.FogInterpolationMode)file.ReadUInt32E();
            heightStart = file.ReadFloat32E();
            heightEnd = file.ReadFloat32E();
            heightMode = (SB09WiiAsset.FogInterpolationMode)file.ReadUInt32E();
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            color.Save(file);
            file.WriteE(start);
            file.WriteE(end);
            file.WriteE((uint)mode);
            file.WriteE(heightStart);
            file.WriteE(heightEnd);
            file.WriteE((uint)heightMode);
            EventLinksNew.Save(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}