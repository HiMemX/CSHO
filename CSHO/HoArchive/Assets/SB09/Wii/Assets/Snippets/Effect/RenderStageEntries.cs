using System.ComponentModel;
using System.Collections.Generic;

namespace SB09WiiAsset{
    public class RenderStageEntries{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public RenderStageEntry element0{get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public RenderStageEntry element1{get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public RenderStageEntry element2{get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public RenderStageEntry element3{get;set;}

        public RenderStageEntries(List<RenderStageEntry> entries){
            element0 = entries[0];
            element1 = entries[1];
            element2 = entries[2];
            element3 = entries[3];
        }
    }
}