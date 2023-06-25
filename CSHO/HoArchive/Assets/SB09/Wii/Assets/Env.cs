using System.ComponentModel;

namespace SB09WiiAsset{
    public class Env : xBaseAsset{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong bspAssetID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong StartCamera {get; set;}
        public uint Climate {get; set;}
        public float ClimateStrengthMin {get; set;}
        public float ClimateStrengthMax {get; set;}
        public uint flags {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong bspCollisionAssetID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong bspFXAssetID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong bspCameraAssetID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong bspMapperID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong bspMapperCollisionID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong bspMapperFXID {get; set;}
        public float AdditionalLOLDHeight {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 MinBounds {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 MaxBounds {get; set;}
        public bool dofEnabled {get; set;}
        public float dofBlur {get; set;}
        public float dofNearFocusPoint {get; set;}
        public float dofNearFocusFalloff {get; set;}
        public float dofFarFocusPoint {get; set;}
        public float dofFarFocusFalloff {get; set;}
        public LinkAsset EventLinksNew;

        public Env(HoArchive.MemoryStreamEndian file) : base(file){
            bspAssetID = file.ReadUInt64E();
            StartCamera = file.ReadUInt64E();
            Climate = file.ReadUInt32E();
            ClimateStrengthMin = file.ReadFloat32E();
            ClimateStrengthMax = file.ReadFloat32E();
            flags = file.ReadUInt32E();
            bspCollisionAssetID = file.ReadUInt64E();
            bspFXAssetID = file.ReadUInt64E();
            bspCameraAssetID = file.ReadUInt64E();
            bspMapperID = file.ReadUInt64E();
            bspMapperCollisionID = file.ReadUInt64E();
            bspMapperFXID = file.ReadUInt64E();
            AdditionalLOLDHeight = file.ReadFloat32E();
            MinBounds = new HoArchive.float3(file);
            MaxBounds = new HoArchive.float3(file);
            dofEnabled = file.ReadBool();
            file.Position += 0x03;
            dofBlur = file.ReadFloat32E();
            dofNearFocusPoint = file.ReadFloat32E();
            dofNearFocusFalloff = file.ReadFloat32E();
            dofFarFocusPoint = file.ReadFloat32E();
            dofFarFocusFalloff = file.ReadFloat32E();
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(bspAssetID);
            file.WriteE(StartCamera);
            file.WriteE(Climate);
            file.WriteE(ClimateStrengthMin);
            file.WriteE(ClimateStrengthMax);
            file.WriteE(flags);
            file.WriteE(bspCollisionAssetID);
            file.WriteE(bspFXAssetID);
            file.WriteE(bspCameraAssetID);
            file.WriteE(bspMapperID);
            file.WriteE(bspMapperCollisionID);
            file.WriteE(bspMapperFXID);
            file.WriteE(AdditionalLOLDHeight);
            MinBounds.Save(file);
            MaxBounds.Save(file);
            file.WriteE(dofEnabled);
            file.Pad(0x03, 0x00);
            file.WriteE(dofBlur);
            file.WriteE(dofNearFocusPoint);
            file.WriteE(dofNearFocusFalloff);
            file.WriteE(dofFarFocusPoint);
            file.WriteE(dofFarFocusFalloff);
            EventLinksNew.Save(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}