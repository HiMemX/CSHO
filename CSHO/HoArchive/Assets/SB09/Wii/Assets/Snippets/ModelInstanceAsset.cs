using System.ComponentModel;

namespace SB09WiiAsset{
    public class ModelInstanceAsset{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong modelPrototypeID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong lightKitID {get; set;}
        public uint instanceParamCount {get; set;}
        public uint renderCustomizerCount {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_instanceParams instanceParams {get; set;} // Pointer32
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_renderCustomizers renderCustomizers {get; set;} // Pointer32
        public ushort shadowType {get; set;}
        public ushort shadowFlags {get; set;}
        [TypeConverter(typeof(HoArchive.RGBA8888Converter))]
        public HoArchive.RGBA8888 shadowColorOverride {get; set;}
        public float shadowMaxDepthOverride {get; set;}
        public float shadowStartDepthOverride {get; set;}
        public uint shadowMinBlurOverride {get; set;}
        public uint shadowMaxBlurOverride {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong parentID {get; set;}

        public ModelInstanceAsset(HoArchive.MemoryStreamEndian file){
            modelPrototypeID = file.ReadUInt64E();
            lightKitID = file.ReadUInt64E();
            instanceParamCount = file.ReadUInt32E();
            renderCustomizerCount = file.ReadUInt32E();
            instanceParams = new Pointer32_instanceParams(file, instanceParamCount);
            renderCustomizers = new Pointer32_renderCustomizers(file, renderCustomizerCount);
            shadowType = file.ReadUInt16E();
            shadowFlags = file.ReadUInt16E();
            shadowColorOverride = new HoArchive.RGBA8888(file);
            shadowMaxDepthOverride = file.ReadFloat32E();
            shadowStartDepthOverride = file.ReadFloat32E();
            shadowMinBlurOverride = file.ReadUInt32E();
            shadowMaxBlurOverride = file.ReadUInt32E();
            parentID = file.ReadUInt64E();
        }

        public void Update(HoArchive.TOCEntry entry){
            instanceParamCount = (uint)instanceParams.instanceParams.Count;
            renderCustomizerCount = (uint)renderCustomizers.renderCustomizers.Count;
            instanceParams.Update();
            renderCustomizers.Update();
            parentID = entry.uidSelf;
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(modelPrototypeID);
            file.WriteE(lightKitID);
            file.WriteE(instanceParamCount);
            file.WriteE(renderCustomizerCount);
            instanceParams.SavePointer(file);
            renderCustomizers.SavePointer(file);
            file.WriteE(shadowType);
            file.WriteE(shadowFlags);
            shadowColorOverride.Save(file);
            file.WriteE(shadowMaxDepthOverride);
            file.WriteE(shadowStartDepthOverride);
            file.WriteE(shadowMinBlurOverride);
            file.WriteE(shadowMaxBlurOverride);
            file.WriteE(parentID);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            instanceParams.Save(file);
            renderCustomizers.Save(file);
        }
    }
}