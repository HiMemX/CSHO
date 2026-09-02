using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;

namespace HoArchive{

    public class ByteBinaryStringConverter : ByteConverter
{
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
        object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is byte b)
        {
            // Convert to 8-bit binary string
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string s)
        {
            s = s.Trim();

            // Allow hex input like "0xAF"
            if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                return Convert.ToByte(s.Substring(2), 16);
            }

            // Otherwise parse as binary
            return Convert.ToByte(s, 2);
        }

        return base.ConvertFrom(context, culture, value);
    }
}

    public class UInt32BinaryStringConverter : UInt32Converter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is uint flags)
            {
                // Convert to 32-bit binary string
                return Convert.ToString(flags, 2).PadLeft(32, '0');
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                s = s.Trim();

                // Allow 0x... hex input too
                if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    return Convert.ToUInt32(s.Substring(2), 16);
                }

                // Otherwise parse as binary
                return Convert.ToUInt32(s, 2);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }


    public class AssetIDConverter : TypeConverter
    {
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
            try
            {
                return Convert.ToUInt64(value as string, 16);
            }
            catch (FormatException)
            {
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

    public class FloatColorRGBConverter: System.ComponentModel.ExpandableObjectConverter
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
                return new FloatColorRGB( byte.Parse( tokens[0]), byte.Parse( tokens[1]), byte.Parse( tokens[2]));
            }
            catch
            {
                return context.PropertyDescriptor.GetValue( context.Instance );
            }
        }

        public override object ConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            FloatColorRGB p = (FloatColorRGB)value;
            return p.r +"; "+ p.g+"; " + p.b;
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

    public class Point4Converter: System.ComponentModel.ExpandableObjectConverter
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
                return new HoArchive.float4( float.Parse( tokens[0], System.Globalization.NumberStyles.AllowDecimalPoint), float.Parse( tokens[1], System.Globalization.NumberStyles.AllowDecimalPoint), float.Parse( tokens[2], System.Globalization.NumberStyles.AllowDecimalPoint), float.Parse( tokens[3], System.Globalization.NumberStyles.AllowDecimalPoint));
            }
            catch
            {
                return context.PropertyDescriptor.GetValue( context.Instance );
            }
        }

        public override object ConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            HoArchive.float4 p = ( HoArchive.float4 ) value;
            return p.x +"; "+ p.y+"; " + p.z + "; " + p.w;
        }
    }

    
    
}