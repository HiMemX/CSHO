


using System;
using System.ComponentModel;
using System.Globalization;

namespace SB09WiiAsset{

    public class TikiTypeConverter : EnumConverter{
        private Type enumType;

        public TikiTypeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(TikiType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (TikiType)Enum.Parse(typeof(TikiType), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((TikiType)value).ToString();
        }
    }

    public enum TikiType{
        Regular = 0x0,
        Explosive = 0x1,
        Hover = 0x2,
        Hint = 0x3,
        Bonus = 0x4,
        Fluid = 0x5
    }
}