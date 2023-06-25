using System.ComponentModel;

namespace SB09WiiAsset{
    public class Wii_MaterialSettings{
        public byte diffuseMapParamIndex {get; set;}
        public byte lightMapParamIndex {get; set;}
        public byte envMapParamIndex {get; set;}
        public byte colorMulParamIndex {get; set;}
        public byte glowMapParamIndex {get; set;}
        public byte envMapScaleParamIndex {get; set;}
        public byte uvSet1ParamIndex {get; set;}
        public byte uvSet2ParamIndex {get; set;}
        public byte alphaMulParamIndex {get; set;}
        public byte ambientScaleParamIndex {get; set;}
        public byte diffuseMap1ParamIndex {get; set;}
        public byte blendMapParamIndex {get; set;}
        public byte textureBlendFactorIndex {get; set;}
        public byte bumpMapParamIndex {get; set;}
        public ushort pad {get; set;}

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
            file.WriteE(envMapParamIndex);
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