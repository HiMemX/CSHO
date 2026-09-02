using System.ComponentModel;
using System;
using System.Globalization;

namespace HoArchive{
    
    public class enPlatformTypeConverter : EnumConverter{
        private Type enumType;

        public enPlatformTypeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(enPlatformType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (enPlatformType)Enum.Parse(typeof(enPlatformType), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((enPlatformType)value).ToString();
        }

    }

    public enum enPlatformType{ // Ich komm nicht weiter lllolololol
        SPLINE = 0,
        MOVEPOINT = 1,
        MECHANISM = 2, // Confident
        CONVEYOR_BELT = 3,
        TEETER = 4, // Confident
        FULLY_MANIPULABLE = 5,
    }
}