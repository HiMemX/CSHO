// This class is purely for ease of editing in Plankton. The purpose is to wrap asset id lists to make them show up as hex numbers in the
// list editors.

using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System;
using HoArchive;

namespace SB09WiiAsset;

[TypeConverter(typeof(AssetIDClassConverter))]
public class AssetID
{
    [TypeConverter(typeof(AssetIDConverter))]
    public ulong uid { get; set; }

    public AssetID() => uid = 0;
    public AssetID(ulong value) => uid = value;

    public override string ToString() => "0x" + uid.ToString("X16");
}

public class AssetIDClassConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is AssetID hex)
                return "0x" + hex.uid.ToString("X16");
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture, object value)
        {
            if (value is string s)
            {
                if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    s = s.Substring(2);

                if (ulong.TryParse(s, NumberStyles.HexNumber, culture, out var ul))
                    return new AssetID(ul);

                throw new FormatException("Invalid hex format");
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

[TypeConverter(typeof(CollectionConverter))]
public class AssetIDList : Collection<AssetID>
{
    public AssetIDList(HoArchive.MemoryStreamEndian file, uint count)
    {
        for (int i = 0; i < count; i++) Add(new AssetID(file.ReadUInt64E()));
    }

    public void Save(HoArchive.MemoryStreamEndian file)
    {

        foreach (AssetID id in this) file.WriteE(id.uid);
    }
}