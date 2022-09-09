
namespace Asset{
    public class ModelInstanceAsset{
        public byte[] modelPrototypeID = new byte[8];
        public byte[] lightKitID = new byte[8];
        public uint instanceParameterCount;
        public uint renderCustomizerCount;
        public uint instanceParams;
        public uint renderCustomizers;
        public ushort shadowType;
        public ushort shadowFlags;
        public uint shadowColorOverride;
        public uint shadowMaxDepthOverride;
        public uint shadowStartDepthOverride;
        public uint shadowMinBlurOverride;
        public uint shadowMaxBlurOverride;
        public byte[] parentID = new byte[8];
    }
}