using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;

namespace HoArchive{


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

    public class RGBA8888Converter: System.ComponentModel.ExpandableObjectConverter
    {
        public override bool CanConvertFrom( System.ComponentModel.ITypeDescriptorContext context, Type sourceType )
        {
            return sourceType == typeof( string );
        }

        public override object ConvertFrom( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
        {
            try
            {
                string[] tokens = (( string ) value).Split( "; ");
                return new HoArchive.RGBA8888( byte.Parse( tokens[0]), byte.Parse( tokens[1]), byte.Parse( tokens[2]), byte.Parse(tokens[3]));
            }
            catch
            {
                return context.PropertyDescriptor.GetValue( context.Instance );
            }
        }

        public override object ConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            HoArchive.RGBA8888 p = ( HoArchive.RGBA8888 ) value;
            return p.r +"; "+ p.g+"; " + p.b + "; " + p.a;
        }
    }

    public class Point3Converter: System.ComponentModel.ExpandableObjectConverter
    {
        public override bool CanConvertFrom( System.ComponentModel.ITypeDescriptorContext context, Type sourceType )
        {
            return sourceType == typeof( string );
        }

        public override object ConvertFrom( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
        {
            try
            {
                string[] tokens = (( string ) value).Split( "; ");
                return new HoArchive.float3( float.Parse( tokens[0], System.Globalization.NumberStyles.AllowDecimalPoint), float.Parse( tokens[1], System.Globalization.NumberStyles.AllowDecimalPoint), float.Parse( tokens[2], System.Globalization.NumberStyles.AllowDecimalPoint) );
            }
            catch
            {
                return context.PropertyDescriptor.GetValue( context.Instance );
            }
        }

        public override object ConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            HoArchive.float3 p = ( HoArchive.float3 ) value;
            return p.x +"; "+ p.y+"; " + p.z;
        }
    }

}