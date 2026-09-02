using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class MaterialParamFormatTableAsset_Array3{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MaterialParamFormatTableAsset element0 {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MaterialParamFormatTableAsset element1 {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MaterialParamFormatTableAsset element2 {get; set;}

        public MaterialParamFormatTableAsset_Array3(List<MaterialParamFormatTableAsset> param){
            element0 = param[0];
            element1 = param[1];
            element2 = param[2];
        }
    }
}