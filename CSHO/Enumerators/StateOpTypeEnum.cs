using System.ComponentModel;
using System;
using System.Globalization;

namespace SB09WiiAsset{
    public class StateOpTypeConverter : EnumConverter{
        private Type enumType;

        public StateOpTypeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(StateOpTypeEnum);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (StateOpTypeEnum)Enum.Parse(typeof(StateOpTypeEnum), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((StateOpTypeEnum)value).ToString();
        }

    }


    public enum StateOpTypeEnum{
        STATEOP_USER_FLOAT4         = 0x00,
        STATEOP_USER_INT4           = 0x01,
        STATEOP_USER_BOOL           = 0x02,
        STATEOP_USER_MATRIX_FLOAT   = 0x03,
        STATEOP_USER_MATRIX_INT     = 0x04,
        STATEOP_USER_SAMPLER        = 0x05,
        STATEOP_VIEW_MAT            = 0x06,
        STATEOP_WORLD_MAT           = 0x07,
        STATEOP_CAM_POS            = 0x08,
        STATEOP_CAM_POS_WORLD        = 0x09,
        STATEOP_EYE_DIR            = 0x0A,
        STATEOP_SMAP_DEPTH_MAT        = 0x0B,
        STATEOP_SMAP_DEPTH_BIAS     = 0x0C,
        STATEOP_RENDER            = 0x0D,
        STATEOP_PROJ_MAT         = 0x0E,
        STATEOP_LIGHT_ARRAY        = 0x0F,
        STATEOP_FOG_PARAMS        = 0x10,
        STATEOP_CAM_PROJ_MAT        = 0x11,
        STATEOP_COLOR_MUL        = 0x12,
        STATEOP_POST_COLOR_MUL        = 0x13,
        STATEOP_USER_FLOAT_ANY        = 0x14,
        STATEOP_BONE_PALETTE        = 0x15,
        STATEOP_BONE_OFFSET        = 0x16,
        STATEOP_LIGHT_FAST        = 0x17,
        STATEOP_PRJ_SHADOW_CAST_MAT = 0x18,
        STATEOP_PRJ_SHADOW        = 0x19,
        STATEOP_PRJ_SHADOW_TEX        = 0x1A,
        STATEOP_PRJ_DECAL        = 0x1B,
        STATEOP_INVCAM_MAT        = 0x1C,
        STATEOP_WII_SHADER        = 0x1D,
        STATEOP_WII_GEOMETRY        = 0x1E,
        STATEOP_WII_MATERIAL        = 0x1F
    }
}