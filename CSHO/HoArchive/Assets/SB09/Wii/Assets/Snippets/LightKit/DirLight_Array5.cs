using System;
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class DirLight_Array5{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DirLight element0 {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DirLight element1 {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DirLight element2 {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DirLight element3 {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DirLight element4 {get; set;}

        public DirLight_Array5(MemoryStreamEndian file){
            element0 = new DirLight(file);
            element1 = new DirLight(file);
            element2 = new DirLight(file);
            element3 = new DirLight(file);
            element4 = new DirLight(file);
        }
        public void Save(MemoryStreamEndian file){
            element0.Save(file);
            element1.Save(file);
            element2.Save(file);
            element3.Save(file);
            element4.Save(file);
        }
    }
}