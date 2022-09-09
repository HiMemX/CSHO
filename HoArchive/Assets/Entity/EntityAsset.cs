
namespace Asset{
    public class EntityAsset : BaseAsset{
        public byte flags;
        public byte subtype;
        public byte pad;
        public byte Targettable;
        public byte Pad0;
        public ushort MoreFlags;
        public byte[] surfaceID = new byte[8];
        public HoArchive.float3 Orientation;
        public HoArchive.float3 Pos;
        public HoArchive.float3 Scale;
        public ModelInstanceAsset modelInstance;
        public byte[] animListID = new byte[8];
        
    }
}