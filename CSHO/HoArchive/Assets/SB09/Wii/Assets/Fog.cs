using System.ComponentModel;

namespace SB09WiiAsset{
    public class Fog : xBaseAsset{
        //[TypeConverter(typeof(HoArchive.RGBA8888Converter))]
        [TypeConverter(typeof(HoArchive.RGBA8888Converter))]
        public HoArchive.RGBA8888 color {get; set;}
        public float start {get; set;}
        public float end {get; set;}
        [TypeConverter(typeof(SB09WiiAsset.FogInterpolationModeConverter))]
        public SB09WiiAsset.FogInterpolationMode mode {get; set;}
        public float heightStart {get; set;}
        public float heightEnd {get; set;}
        [TypeConverter(typeof(SB09WiiAsset.FogInterpolationModeConverter))]
        public SB09WiiAsset.FogInterpolationMode heightMode {get; set;}
        public LinkAsset EventLinksNew {get; set;}

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