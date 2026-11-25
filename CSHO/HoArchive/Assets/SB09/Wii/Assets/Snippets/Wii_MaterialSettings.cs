using System.ComponentModel;

namespace SB09WiiAsset{
    public class Wii_MaterialSettings{
        public byte diffuseMapParamIndex {get; set;} = new();
        public byte lightMapParamIndex {get; set;} = new();
        public byte envMapParamIndex {get; set;} = new();
        public byte colorMulParamIndex {get; set;} = new();
        public byte glowMapParamIndex {get; set;} = new();
        public byte envMapScaleParamIndex {get; set;} = new();
        public byte uvSet1ParamIndex {get; set;} = new();
        public byte uvSet2ParamIndex {get; set;} = new();
        public byte alphaMulParamIndex {get; set;} = new();
        public byte ambientScaleParamIndex {get; set;} = new();
        public byte diffuseMap1ParamIndex {get; set;} = new();
        public byte blendMapParamIndex {get; set;} = new();
        public byte textureBlendFactorIndex {get; set;} = new();
        public byte bumpMapParamIndex {get; set;} = new();
        public ushort pad {get; set;} = new();

        public Wii_MaterialSettings(){}

        public Wii_MaterialSettings(HoArchive.MemoryStreamEndian file){
            diffuseMapParamIndex = file.ReadByte();
            lightMapParamIndex = file.ReadByte();
            envMapParamIndex = file.ReadByte();
            colorMulParamIndex = file.ReadByte();
            glowMapParamIndex = file.ReadByte();
            envMapScaleParamIndex = file.ReadByte();
            uvSet1ParamIndex = file.ReadByte();
            uvSet2ParamIndex = file.ReadByte();
            alphaMulParamIndex = file.ReadByte();
            ambientScaleParamIndex = file.ReadByte();
            diffuseMap1ParamIndex = file.ReadByte();
            blendMapParamIndex = file.ReadByte();
            textureBlendFactorIndex = file.ReadByte();
            bumpMapParamIndex = file.ReadByte();
            pad = file.ReadUInt16();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(diffuseMapParamIndex);
            file.WriteE(lightMapParamIndex);
            file.WriteE(envMapParamIndex);
            file.WriteE(colorMulParamIndex);
            file.WriteE(glowMapParamIndex);
            file.WriteE(envMapScaleParamIndex);
            file.WriteE(uvSet1ParamIndex);
            file.WriteE(uvSet2ParamIndex);
            file.WriteE(alphaMulParamIndex);
            file.WriteE(ambientScaleParamIndex);
            file.WriteE(diffuseMap1ParamIndex);
            file.WriteE(blendMapParamIndex);
            file.WriteE(textureBlendFactorIndex);
            file.WriteE(bumpMapParamIndex);
            file.WriteE(pad);
        }
    }
}