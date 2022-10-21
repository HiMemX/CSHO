using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;

namespace HoArchive{
    
    public class wmlTypeIDConverter : TypeConverter{
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(uint);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Asset.AssetKey key = new Asset.AssetKey();

            foreach(KeyValuePair<uint, string> entry in key.key){
                if(entry.Value == value as string){
                    return entry.Key;
                }
            }
            try{
                return Convert.ToUInt32(value as string, 16);
            }
            catch(Exception){
                return 0;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Asset.AssetKey key = new Asset.AssetKey();
            if(key.Contains((uint)value)){
                return key.key[(uint)value];
            }

            return "0x" + ((uint)value).ToString("X8");
        }
    }


    public class AssetIDConverter : TypeConverter{
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(ulong) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            try{
                return Convert.ToUInt64(value as string, 16);
            }
            catch(FormatException){
                return 0;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            string hexstring = "0x" + ((ulong)value).ToString("X16");

            return hexstring ?? base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class UIntConverter : TypeConverter{
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(uint) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            try{
                return Convert.ToUInt32(value as string, 16);
            }
            catch(FormatException){
                return 0;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            string hexstring = "0x" + ((uint)value).ToString("X8");

            return hexstring ?? base.ConvertTo(context, culture, value, destinationType);
        }
    }
    public class UShortConverter : TypeConverter{
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(ushort) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            try{
                return Convert.ToUInt16(value as string, 16);
            }
            catch(FormatException){
                return 0;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            string hexstring = "0x" + ((ushort)value).ToString("X4");

            return hexstring ?? base.ConvertTo(context, culture, value, destinationType);
        }
    }

}