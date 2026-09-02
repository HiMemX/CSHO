using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class PuckReflector : xBaseAsset{
        [TypeConverter(typeof(Point3Converter))]
        public float3 rot {get;set;} = new float3(0,0,0);
        
        [TypeConverter(typeof(Point3Converter))]
        public float3 pos {get;set;} = new float3(0,0,0);
        
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ModelInstanceAsset modelInstance {get;set;}// = new ModelInstanceAsset();
        
        [TypeConverter(typeof(Point3Converter))]
        public float3 Scale {get;set;} = new float3(0,0,0);

        [TypeConverter(typeof(AssetIDConverter))]
        public ulong reflectTarget {get;set;} = 0;

        [TypeConverter(typeof(Point3Converter))]
        public float3 reflectDirection {get;set;} = new float3(0,0,0);

        public float yawRange {get;set;} = 0;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew;

        public PuckReflector(HoArchive.MemoryStreamEndian file) : base(file)
        {
            rot = new float3(file);
            pos = new float3(file);
            file.Position += 8;
            modelInstance = new ModelInstanceAsset(file);
            file.Position = 0x70;
            Scale = new float3(file);
            file.Position = 0x80;
            reflectTarget = file.ReadUInt64E();
            reflectDirection = new float3(file);
            yawRange = file.ReadFloat32E();
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry)
        {
            base.Update(entry);
            EventLinksNew.Update();
            modelInstance.Update(entry);
        }

        public override void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);
            rot.Save(file);
            pos.Save(file);
            file.Pad(8, 0);
            modelInstance.Save(file);
            file.PadAlign(0x10, 0x0);
            Scale.Save(file);
            file.PadAlign(0x10, 0);
            file.WriteE(reflectTarget);
            reflectDirection.Save(file);
            file.WriteE(yawRange);
            EventLinksNew.Save(file);


            modelInstance.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}