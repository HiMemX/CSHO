using System.ComponentModel;
using System;
using System.Globalization;

namespace HoArchive{
    
    public class PrimitiveTypeConverter : EnumConverter{
        private Type enumType;

        public PrimitiveTypeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(PrimitiveType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (PrimitiveType)Enum.Parse(typeof(PrimitiveType), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((PrimitiveType)value).ToString();
        }

    }

    public enum PrimitiveType{
        PRIM_LINELIST = 0xA8,
        PRIM_LINESTRIP = 0xB0,
        PRIM_POINTLIST = 0xB8,
        PRIM_QUADLIST = 0x80,
        PRIM_TRIFAN = 0xA0,
        PRIM_TRILIST = 0x90,
        PRIM_TRISTRIP = 0x98,
    }
}