using System;
using System.ComponentModel;
using System.Globalization;

namespace HoArchive{

    public class LanguageIDConverter : EnumConverter{
        private Type enumType;

        public LanguageIDConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(LanguageID);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (LanguageID)Enum.Parse(typeof(LanguageID), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((LanguageID)value).ToString();
        }

    }

    public enum LanguageID{
        Neutral = 0,
        Arabic = 1,
        ArabicSaudiArabia = 0x401,
        Chinese = 0x4,
        ChineseChina = 0x804,
        ChineseHongKong = 0xC04,
        ChineseTaiwan = 0x404,
        ChineseSingapore = 0x1004,
        Czech = 0x5,
        CzechRepublic = 0x405,
        Danish = 0x6,
        DanishDenmark = 0x406,
        Dutch = 0x13,
        DutchNetherlands = 0x413,
        English = 0x9,
        EnglishUS = 0x409,
        EnglishUK = 0x809,
        EnglishAustralia = 0xC09,
        EnglishCanada = 0x1009,
        Finnish = 0xb,
        FinnishFinland = 0x40b,
        French = 0xc,
        FrenchFrance = 0x40c,
        German = 0x7,
        GermanGermany = 0x407,
        GermanSwiss = 0x807,
        Greek = 0x8,
        GreekGreece = 0x408,
        Italian = 0x10,
        ItalianItaly = 0x410,
        Japanese = 0x11,
        JapaneseJapan = 0x411,
        Korean = 0x12,
        KoreanKorea = 0x412,
        Norwegian = 0x14,
        NorwegianNynorsk = 0x814,
        Polish = 0x15,
        PolishPoland = 0x415,
        Portugese = 0x16,
        PortugesePortugal = 0x816,
        PortugeseBrazilian = 0x416,
        Russian = 0x19,
        RussianFederation = 0x419,
        Slovak = 0x1b,
        SlovakSlovak = 0x41b,
        Spanish = 0xa,
        SpanishMexican = 0x80a,
        SpanishSpain = 0xc0a,
        Swedish = 0x1d,
        SwedishSweden = 0x41d,
        Ukrainian = 0x22,
        UkrainianUkraine = 0x422,
    }
}