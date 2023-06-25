
using System;
using System.ComponentModel;
using System.Globalization;
using System.Collections.Generic;



namespace HoArchive{
    public class SB09WiiPaddedStringPointerConverter: System.ComponentModel.ExpandableObjectConverter
    {
        public override bool CanConvertFrom( System.ComponentModel.ITypeDescriptorContext context, Type sourceType )
        {
            return sourceType == typeof( string );
        }

        public override object ConvertFrom( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
        {
            return new SB09WiiAsset.Pointer32_paddedString((string)value);
        }

        public override object ConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            return ((SB09WiiAsset.Pointer32_paddedString)value).str;
        }
    }





    public class SB09WiiSoundIndicesPointerConverter: System.ComponentModel.ExpandableObjectConverter
    {
        public override bool CanConvertFrom( System.ComponentModel.ITypeDescriptorContext context, Type sourceType )
        {
            return sourceType == typeof( string );
        }

        public override object ConvertFrom( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
        {
            List<string> tokens = new List<string>((( string ) value).Split("/")[1..^0]);
            SB09WiiAsset.soundIndices indices = new SB09WiiAsset.soundIndices();
            for(int i=0; i<tokens.Count-1; i++){
                indices.path.Add(new SB09WiiAsset.soundIndex(false, Byte.Parse(tokens[i])));
            }
            indices.path.Add(new SB09WiiAsset.soundIndex(true, Byte.Parse(tokens[tokens.Count-1])));

            return new SB09WiiAsset.Pointer32_soundIndices(indices);
        }

        public override object ConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            SB09WiiAsset.soundIndices p = (( SB09WiiAsset.Pointer32_soundIndices ) value).indices;
            string output = "";
            foreach(SB09WiiAsset.soundIndex index in p.path){
                output += ("/" + index.index);
            }
            return output;
        }
    }

  

    
}