using System.ComponentModel;
using System.Collections.Generic;

namespace SB09WiiAsset{
    public class ModelInstanceAsset{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong modelPrototypeID {get; set;} = 0;
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong lightKitID {get; set;} = 0;
        public uint instanceParamCount {get; set;} = 0;
        public uint renderCustomizerCount {get; set;} = 0;
        
        public Pointer32_instanceParams instanceParams; // Pointer32
        public Pointer32_renderCustomizers renderCustomizers; // Pointer32
        public List<instanceParam> _instanceParams {get {return instanceParams.instanceParams;} set{instanceParams.instanceParams = value;}}
        public List<renderCustomizer> _renderCustomizers {get {return renderCustomizers.renderCustomizers;} set{renderCustomizers.renderCustomizers = value;}}

        public ushort shadowType {get; set;} = 0;
        public ushort shadowFlags {get; set;} = 0;
        [TypeConverter(typeof(HoArchive.RGBA8888Converter))]
        public HoArchive.RGBA8888 shadowColorOverride {get; set;}
        public float shadowMaxDepthOverride {get; set;} = 0;
        public float shadowStartDepthOverride {get; set;} = 0;
        public uint shadowMinBlurOverride {get; set;} = 0;
        public uint shadowMaxBlurOverride {get; set;} = 0;
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong parentID {get; set;} = 0;

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