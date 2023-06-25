using System.ComponentModel;

namespace SB09WiiAsset{
    public class GenericShader : ShaderAsset{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_Wii_ShaderStateOp shaderOps {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_Wii_MaterialStateOp materialOps {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_Wii_GeometryStateOp geomOps {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_Wii_MaterialStateOp rendOps {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_MaterialParamFormatTableAsset shaderParamFormats {get; set;}
        public int featureFlags {get; set;}
        public uint flags {get; set;}


        public GenericShader(HoArchive.MemoryStreamEndian file) : base(file){
            shaderOps = new Pointer32_Wii_ShaderStateOp(file);
            materialOps = new Pointer32_Wii_MaterialStateOp(file);
            geomOps = new Pointer32_Wii_GeometryStateOp(file);
            rendOps = new Pointer32_Wii_MaterialStateOp(file);
            shaderParamFormats = new Pointer32_MaterialParamFormatTableAsset(file);
            featureFlags = file.ReadInt32E();
            flags = file.ReadUInt32E();
        }

        public override void Update(HoArchive.TOCEntry entry){
            shaderOps.Update();
            materialOps.Update();
            geomOps.Update();
            rendOps.Update();
            shaderParamFormats.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            shaderOps.SavePointer(file);
            materialOps.SavePointer(file);
            geomOps.SavePointer(file);
            rendOps.SavePointer(file);
            shaderParamFormats.SavePointer(file);
            file.WriteE(featureFlags);
            file.WriteE(flags);

            shaderParamFormats.Save(file);
            shaderOps.Save(file);
            materialOps.Save(file);
            geomOps.Save(file);
            rendOps.Save(file);

            file.PadAlign(0x10, 0x00);

        }
    }
}