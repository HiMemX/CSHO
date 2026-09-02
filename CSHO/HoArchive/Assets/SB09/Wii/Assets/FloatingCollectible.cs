using System;
using System.ComponentModel;
using System.Numerics;
using HoArchive;

namespace SB09WiiAsset{
    public class FloatingCollectible : xBaseAsset{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ModelInstanceAsset ModelInstance {get; set;}
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public HoArchive.float3 Position {get; set;}
        public float CollectDistance {get;set;}
        public float SB09CollectTime {get;set;}
        public float SB09CollectHeight {get;set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong VFXSpawn {get;set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong VFXCollectSpawn {get;set;}
        public bool HasBalloon {get;set;}
        public bool UseGeneric {get;set;}
        public bool InitiallyHidden {get;set;}
        public bool RandomInitialRotation {get;set;}
        public uint MotionType {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public uGameName uItemType {get;set;} // Name is different
        [TypeConverter(typeof(GameNameConverter))]
        public GameName gameName {get;set;}
        public bool isDrivenBy {get;set;} // Different from DWARF
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong DrivenByObject {get;set;}
        
        

        public FloatingCollectible(HoArchive.MemoryStreamEndian file) : base(file){
            EventLinksNew = new LinkAsset(file);
            file.Align(0x10);
            ModelInstance = new ModelInstanceAsset(file);
            Position = new float3(file);
            CollectDistance = file.ReadFloat32E();
            SB09CollectTime = file.ReadFloat32E();
            SB09CollectHeight = file.ReadFloat32E();
            VFXSpawn = file.ReadUInt64E();
            VFXCollectSpawn = file.ReadUInt64E();
            HasBalloon = file.ReadBool();
            UseGeneric = file.ReadBool();
            InitiallyHidden = file.ReadBool();
            RandomInitialRotation = file.ReadBool();
            MotionType = file.ReadUInt32E();

            file.Position += 0x04;
            gameName = (GameName)file.ReadUInt32E();
            file.Position -= 0x08;
            uItemType = new uGameName(file, gameName);
            file.Position += 0x04;


            isDrivenBy = file.ReadBool();
            file.Align(0x10);
            DrivenByObject = file.ReadUInt64E();
            
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            ModelInstance.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            
            EventLinksNew.Save(file);
            file.PadAlign(0x10, 0);
            ModelInstance.Save(file);
            Position.Save(file);
            file.WriteE(CollectDistance);
            file.WriteE(SB09CollectTime);
            file.WriteE(SB09CollectHeight);
            file.WriteE(VFXSpawn);
            file.WriteE(VFXCollectSpawn);
            file.WriteE(HasBalloon);
            file.WriteE(UseGeneric);
            file.WriteE(InitiallyHidden);
            file.WriteE(RandomInitialRotation);
            file.WriteE(MotionType);

            uItemType.Save(file, gameName);
            file.WriteE((uint)gameName);

            file.WriteE(isDrivenBy);
            file.PadAlign(0x10, 0);
            file.WriteE(DrivenByObject);
            file.PadAlign(0x10, 0);

            EventLinksNew.SaveHeap(file);
            ModelInstance.SaveHeap(file);
        }

        public Matrix4x4 GetInstanceMatrix()
        {
            
            return Matrix4x4.CreateTranslation(Position.GetVector3());
        }
    }
}