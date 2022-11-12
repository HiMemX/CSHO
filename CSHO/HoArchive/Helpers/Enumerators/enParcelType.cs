using System.ComponentModel;
using System;
using System.Globalization;

namespace HoArchive{
    
    public class enParcelTypeConverter : EnumConverter{
        private Type enumType;

        public enParcelTypeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(enParcelType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (enParcelType)Enum.Parse(typeof(enParcelType), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((enParcelType)value).ToString();
        }

    }

    public enum enParcelType{
        PARCEL_TYPE_UNDEFINED = 0,
        PARCEL_TYPE_EXCLUSIVE = 1,
        PARCEL_TYPE_SHARED = 2,
        PARCEL_TYPE_FROMDOMAIN = 3,
    }
}