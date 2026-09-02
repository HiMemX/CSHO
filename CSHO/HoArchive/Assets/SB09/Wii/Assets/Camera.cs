using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class Camera : xBaseAsset{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public uTargetMode uTargetMode {get; set;}
        [TypeConverter(typeof(TargetModeConverter))]
        public TargetMode TargetMode {get; set;}
        [TypeConverter(typeof(Point3Converter))]
        public float3 pos {get; set;}
        [TypeConverter(typeof(Point3Converter))]
        public float3 at {get; set;}
        [TypeConverter(typeof(Point3Converter))]
        public float3 up {get; set;}
        [TypeConverter(typeof(Point3Converter))]
        public float3 right {get; set;}
        [TypeConverter(typeof(Point3Converter))]
        public float3 orientation {get; set;}
        public float fov {get; set;}
        public float trans_time {get; set;}
        public uint flags {get; set;}
        public bool dofEnabled {get; set;}
        public float dofBlur {get; set;}
        public float dofNearFocusPoint {get; set;}
        public float dofNearFocusFalloff{get; set;}
        public float dofFarFocusPoint {get; set;}
        public float dofFarFocusFalloff{get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;}


        public Camera(HoArchive.MemoryStreamEndian file) : base(file){
            uTargetMode = new uTargetMode(file);
            TargetMode = (TargetMode)file.ReadUInt32E();
            pos = new float3(file);
            at = new float3(file);
            up = new float3(file);
            right = new float3(file);
            orientation = new float3(file);
            fov = file.ReadFloat32E();
            trans_time = file.ReadFloat32E();
            flags = file.ReadUInt32E();
            dofEnabled = file.ReadBool();
            file.Align(0x04);
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
            uTargetMode.Save(file, TargetMode);
            file.WriteE((uint)TargetMode);
            pos.Save(file);
            at.Save(file);
            up.Save(file);
            right.Save(file);
            orientation.Save(file);
            file.WriteE(fov);
            file.WriteE(trans_time);
            file.WriteE(flags);
            file.WriteE(dofEnabled);
            file.Align(0x04);
            file.WriteE(dofBlur);
            file.WriteE(dofNearFocusPoint);
            file.WriteE(dofNearFocusFalloff);
            file.WriteE(dofFarFocusPoint);
            file.WriteE(dofFarFocusFalloff);
            EventLinksNew.Save(file);
            file.Align(0x10);
            EventLinksNew.SaveHeap(file);    
        }
    }
}