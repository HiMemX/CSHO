using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using HoArchive;

namespace SB09WiiAsset{
    public class Pointer32_b_Array3{
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_b _element0; // Bit of a stielbruch but I just like it better this way
        public Pointer32_b _element1;
        public Pointer32_b _element2;

        public List<byte> element0 {get {return _element0.b;} set{_element0.b = value;}}
        public List<byte> element1 {get {return _element1.b;} set{_element1.b = value;}}
        public List<byte> element2 {get {return _element2.b;} set{_element2.b = value;}}

        public Pointer32_b_Array3(List<Pointer32_b> elements){
            _element0 = elements[0];
            _element1 = elements[1];
            _element2 = elements[2];
        }
    }
}