

using System;
using System.ComponentModel;
using System.Globalization;

namespace SB09WiiAsset{

    public class TargetModeConverter : EnumConverter{
        private Type enumType;

        public TargetModeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(TargetMode);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (TargetMode)Enum.Parse(typeof(TargetMode), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((TargetMode)value).ToString();
        }
    }

    public enum TargetMode{
        Rotation = 0x0,
        Target = 0x1,
    }
}