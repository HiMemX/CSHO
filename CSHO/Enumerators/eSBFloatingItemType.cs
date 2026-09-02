using System.ComponentModel;
using System;
using System.Globalization;

namespace HoArchive{
    
    public class eSBFloatingItemTypeConverter : EnumConverter{
        private Type enumType;

        public eSBFloatingItemTypeConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(eSBFloatingItemType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (eSBFloatingItemType)Enum.Parse(typeof(eSBFloatingItemType), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((eSBFloatingItemType)value).ToString();
        }

    }

    public enum eSBFloatingItemType{
        eSBFloatingItemType_PlaceHolder = 0,
        eSBFloatingItemType_HappyNugget = 1,
        eSBFloatingItemType_UnhappyNugget = 2,
        eSBFloatingItemType_Health = 3,
        eSBFloatingItemType_Life = 4,
        eSBFloatingItemType_PuckAmmo = 5,
        eSBFloatingItemType_SpongeBuffPowerup = 6,
        eSBFloatingItemType_SpinPowerup = 7,
        eSBFloatingItemType_HammerPowerup = 8,
        eSBFloatingItemType_PuckPowerup = 9,
        eSBFloatingItemType_InvincibilityPowerup = 10,
        eSBFloatingItemType_Key = 11,
        eSBFloatingItemType_MemoryObject = 12,
        eSBFloatingItemType_BonusFeature = 13,
    }
}