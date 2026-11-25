using System.ComponentModel;

namespace SB09WiiAsset{
    public class SimpleObject : xEntAsset{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong Destructible {get; set;} = new();
        public float AnimationSpeed {get; set;} = new();
        public uint InitialState {get; set;} = new();
        public byte Collision {get; set;} = new();
        public bool CameraCollisionOff {get; set;} = new();
        public bool GoTransparent {get; set;} = new();
        public byte SimpleFlags {get; set;} = new();
        public bool ForceUpdateDistance {get; set;}  = new();// bool
        public float ForceUpdateDistanceValue {get; set;} = new();
        public byte orientRef {get; set;} = new();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;} = new();

        public SimpleObject() : base(){}

        public SimpleObject(HoArchive.MemoryStreamEndian file) : base(file){
            Destructible = file.ReadUInt64E();
            AnimationSpeed = file.ReadFloat32E();
            InitialState = file.ReadUInt32E();
            Collision = file.ReadByte();
            CameraCollisionOff = file.ReadBool();
            GoTransparent = file.ReadBool();
            SimpleFlags = file.ReadByte();
            ForceUpdateDistance = file.ReadBool();
            file.Position += 0x03;
            ForceUpdateDistanceValue = file.ReadFloat32E();
            orientRef = file.ReadByte();
            file.Position += 0x03;
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(Destructible);
            file.WriteE(AnimationSpeed);
            file.WriteE(InitialState);
            file.WriteE(Collision);
            file.WriteE(CameraCollisionOff);
            file.WriteE(GoTransparent);
            file.WriteE(SimpleFlags);
            file.WriteE(ForceUpdateDistance);
            file.Pad(0x03, 0x00);
            file.WriteE(ForceUpdateDistanceValue);
            file.WriteE(orientRef);
            file.Pad(0x03, 0x00);
            EventLinksNew.Save(file);

            base.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}