using System.ComponentModel;
using HoArchive;
using RenderingInternal;

namespace SB09WiiAsset{
    public class GenericShader : ShaderAsset{
        public Pointer32_Wii_ShaderStateOp shaderOps;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_ShaderStateOp _shaderOps {get {return shaderOps.shaderStateOp;} set{shaderOps.shaderStateOp = value;}}

        public Pointer32_Wii_MaterialStateOp materialOps;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_MaterialStateOp _materialOps {get {return materialOps.materialStateOp;} set{materialOps.materialStateOp = value;}}

        public Pointer32_Wii_GeometryStateOp geomOps;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_GeometryStateOp _geomOps {get {return geomOps.geometryStateOp;} set{geomOps.geometryStateOp = value;}}

        public Pointer32_Wii_MaterialStateOp rendOps;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_MaterialStateOp _rendOps {get {return rendOps.materialStateOp;} set{rendOps.materialStateOp = value;}}

        public Pointer32_MaterialParamFormatTableAsset shaderParamFormats {get;set;}
        
        public int featureFlags {get; set;}
        
        [TypeConverter(typeof(UInt32BinaryStringConverter))]
        public uint flags { get; set; }


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
            base.Update(entry);

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

            file.PadAlign(0x02, 0xFF);
            shaderParamFormats.Save(file);
            file.PadAlign(0x02, 0xFF);
            shaderOps.Save(file);
            file.PadAlign(0x02, 0xFF);
            materialOps.Save(file);
            file.PadAlign(0x02, 0xFF);
            geomOps.Save(file);
            file.PadAlign(0x02, 0xFF);
            rendOps.Save(file);

            file.PadAlign(0x10, 0x00);

        }

        public ShaderInfo GetShaderInfo()
        {
            ShaderInfo output = new ShaderInfo();
            
            output.position = geomOps.geometryStateOp.pos.GetChannelInfo();
            output.normal   = geomOps.geometryStateOp.norm.GetChannelInfo();
            output.color    = geomOps.geometryStateOp.color.GetChannelInfo();
            output.uvs[0]   = geomOps.geometryStateOp.uv[0].GetChannelInfo();
            output.uvs[1]   = geomOps.geometryStateOp.uv[1].GetChannelInfo();
            output.uvs[2]   = geomOps.geometryStateOp.uv[2].GetChannelInfo();

            output.materialSettings.diffuseMapIndex = materialOps.materialStateOp.mat.diffuseMapParamIndex;
            output.materialSettings.lightMapIndex = materialOps.materialStateOp.mat.lightMapParamIndex;
            output.materialSettings.diffuseMap1Index = materialOps.materialStateOp.mat.diffuseMap1ParamIndex;

            output.rendParamSettings.diffuseMapIndex = rendOps.materialStateOp.mat.diffuseMapParamIndex;
            output.rendParamSettings.lightMapIndex = rendOps.materialStateOp.mat.lightMapParamIndex;
            output.rendParamSettings.diffuseMap1Index = rendOps.materialStateOp.mat.diffuseMap1ParamIndex;

            return output;

        }
    }
}