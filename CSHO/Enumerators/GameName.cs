using System.ComponentModel;
using System;
using System.Globalization;

namespace HoArchive{
    
    public class GameNameConverter : EnumConverter{
        private Type enumType;

        public GameNameConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(GameName);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (GameName)Enum.Parse(typeof(GameName), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((GameName)value).ToString();
        }

    }

    public enum GameName{
        UP09 = 0,
        SB09 = 1,
    }
}