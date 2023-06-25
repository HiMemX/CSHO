using System.ComponentModel;

namespace SB09WiiAsset{
    public class xEntAsset : xBaseAsset{
        public byte flags {get; set;}
        public byte subtype {get; set;}
        public byte pad {get; set;}
        public bool Targettable {get; set;}
        public byte Pad0 {get; set;}
        public byte MoreFlags {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong surfaceID {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 Orientation {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 Pos {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 Scale {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ModelInstanceAsset modelInstance {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong animListID {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PhysicsDataStruct physicsData {get; set;}
        public uint Pad1 {get; set;}
        public uint Pad2 {get; set;}
        public uint Pad3 {get; set;}

        public xEntAsset(HoArchive.MemoryStreamEndian file) : base(file){
            flags = file.ReadByte();
            subtype = file.ReadByte();
            pad = file.ReadByte();
            Targettable = file.ReadBool();
            Pad0 = file.ReadByte();
            MoreFlags = file.ReadByte();
            file.Position = 0x18;
            surfaceID = file.ReadUInt64E();
            Orientation = new HoArchive.float3(file);
            Pos = new HoArchive.float3(file);
            Scale = new HoArchive.float3(file);
            file.Position = 0x50;
            modelInstance = new ModelInstanceAsset(file);
            animListID = file.ReadUInt64E();
            physicsData = new PhysicsDataStruct(file);
            Pad1 = file.ReadUInt32E();
            Pad2 = file.ReadUInt32E();
            Pad3 = file.ReadUInt32E();
            
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            modelInstance.Update(entry);
            physicsData.Update(entry);
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(flags);
            file.WriteE(subtype);
            file.WriteE(pad);
            file.WriteE(Targettable);
            file.WriteE(Pad0);
            file.WriteE(MoreFlags);
            file.PadTo(0x18, 0x00);
            file.WriteE(surfaceID);
            Orientation.Save(file);
            Pos.Save(file);
            Scale.Save(file);
            file.PadTo(0x50, 0x00);
            modelInstance.Save(file);
            file.WriteE(animListID);
            physicsData.Save(file);
            file.WriteE(Pad1);
            file.WriteE(Pad2);
            file.WriteE(Pad3);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            modelInstance.SaveHeap(file);
            physicsData.SaveHeap(file);
        }
    }
}