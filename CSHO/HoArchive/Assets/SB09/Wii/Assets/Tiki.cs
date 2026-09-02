using System.ComponentModel;
using System.Numerics;

namespace SB09WiiAsset{
    public class Tiki : xBaseAsset{
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 Orientation {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 Pos {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 Scale {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ModelInstanceAsset modelInstance {get; set;}
        [TypeConverter(typeof(TikiTypeConverter))]
        public TikiType type {get; set;}
        public bool hover {get; set;}
        public float distanceActivation {get; set;}
        public float distanceExplosionActivation {get; set;}
        public float explosionCountdownFromProximity {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong LandSoundFX {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong collectibles {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong collectiblesHit {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong collectiblesPlankton {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong collectiblesPlanktonHit {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong destructible {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong Animations {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;}


        public Tiki(HoArchive.MemoryStreamEndian file) : base(file){
            Orientation = new HoArchive.float3(file);
            Pos = new HoArchive.float3(file);
            Scale = new HoArchive.float3(file);
            file.Position = 0x40;
            modelInstance = new ModelInstanceAsset(file);
            type = (TikiType)file.ReadUInt32E();
            hover = file.ReadBool();
            file.Position += 0x03;
            distanceActivation = file.ReadFloat32E();
            distanceExplosionActivation = file.ReadFloat32E();
            explosionCountdownFromProximity = file.ReadFloat32E();
            file.Position += 0x04;
            LandSoundFX = file.ReadUInt64E();
            collectibles = file.ReadUInt64E();
            collectiblesHit = file.ReadUInt64E();
            collectiblesPlankton = file.ReadUInt64E();
            collectiblesPlanktonHit = file.ReadUInt64E();
            destructible = file.ReadUInt64E();
            Animations = file.ReadUInt64E();
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            modelInstance.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            Orientation.Save(file);
            Pos.Save(file);
            Scale.Save(file);
            file.PadTo(0x40, 0x00);
            modelInstance.Save(file);
            file.WriteE((uint)type);
            file.WriteE(hover);
            file.Pad(0x03, 0x00);
            file.WriteE(distanceActivation);
            file.WriteE(distanceExplosionActivation);
            file.WriteE(explosionCountdownFromProximity);
            file.Pad(0x04, 0x00);
            file.WriteE(LandSoundFX);
            file.WriteE(collectibles);
            file.WriteE(collectiblesHit);
            file.WriteE(collectiblesPlankton);
            file.WriteE(collectiblesPlanktonHit);
            file.WriteE(destructible);
            file.WriteE(Animations);
            EventLinksNew.Save(file);
            file.Pad(0x08, 0x00);

            modelInstance.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }

        public ulong GetModelID()
        {
            return modelInstance.modelPrototypeID;
        }

        public Matrix4x4 GetInstanceMatrix()
        {
            Matrix4x4 scalemat =  Matrix4x4.CreateScale(new Vector3(Scale.x, Scale.y, Scale.z));
            Matrix4x4 posmat = Matrix4x4.CreateTranslation(new Vector3(Pos.x, Pos.y, Pos.z));
            Matrix4x4 rotmat = Matrix4x4.CreateFromYawPitchRoll(Orientation.x, Orientation.y, Orientation.z);

            return (scalemat * rotmat) * posmat;
        }
        public Vector3 GetPosition()
        {
            return new Vector3(Pos.x, Pos.y, Pos.z);
            return new Vector3(Pos.x, Pos.y, Pos.z);
        }
    }
}