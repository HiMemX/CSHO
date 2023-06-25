
using System;
using System.ComponentModel;
using System.Globalization;

namespace SB09WiiAsset{

    public class FogInterpolationModeConverter : EnumConverter{
        private Type enumType;

        public FogInterpolationModeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(FogInterpolationMode);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (FogInterpolationMode)Enum.Parse(typeof(FogInterpolationMode), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((FogInterpolationMode)value).ToString();
        }
    }

    public enum FogInterpolationMode{
        FOGMODE_None = 0x0,
        FOGMODE_Linear = 0x1,
        FOGMODE_Exponential = 0x2,
        FOGMODE_Exponential2 = 0x3,
        FOGMODE_InverseExponential = 0x4,
        FOGMODE_InverseExponential2 = 0x5,
    }
}