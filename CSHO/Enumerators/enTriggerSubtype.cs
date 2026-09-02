using System.ComponentModel;
using System;
using System.Globalization;

namespace HoArchive{
    
    public class enTriggerSubtypeConverter : EnumConverter{
        private Type enumType;

        public enTriggerSubtypeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(enTriggerSubtype);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (enTriggerSubtype)Enum.Parse(typeof(enTriggerSubtype), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((enTriggerSubtype)value).ToString();
        }

    }

    public enum enTriggerSubtype{
        BOX = 0,
        SPHERE = 1,
        CYLINDER = 2,
        CIRCLE = 3,
        POLYGON = 4
    }
}