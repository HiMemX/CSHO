using System.ComponentModel;

namespace SB09WiiAsset{
    public class SimpleObject : xEntAsset{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong Destructible {get; set;}
        public float AnimationSpeed {get; set;}
        public uint InitialState {get; set;}
        public byte Collision {get; set;}
        public bool CameraCollisionOff {get; set;}
        public bool GoTransparent {get; set;}
        public byte SimpleFlags {get; set;}
        public bool ForceUpdateDistance {get; set;} // bool
        public float ForceUpdateDistanceValue {get; set;}
        public byte orientRef {get; set;}
        public LinkAsset EventLinksNew {get; set;}

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