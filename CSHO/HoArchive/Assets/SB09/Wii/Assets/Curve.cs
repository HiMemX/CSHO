using System;
using System.Collections.Generic;
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class Curve : Asset.AssetEntity{
        public int ClosedCurve { get; set; }
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public float3 LowBounds { get; set; }
        [TypeConverter(typeof(HoArchive.Point3Converter))]
        public float3 HighBounds { get; set; }
        
        public Pointer32_float3s Coefficients;
        public Pointer32_f SegmentLengths;
        public Pointer32_b ParamValues;

        public List<float3> _Coefficients {get {return Coefficients.coefficients;} set {Coefficients.coefficients = value;}}
        public List<float> _SegmentLengths {get {return SegmentLengths.f;} set {SegmentLengths.f = value;}}
        public List<byte> _ParamValues {get {return ParamValues.b;} set {ParamValues.b = value;}}



        public Curve(HoArchive.MemoryStreamEndian file)
        {
            ClosedCurve = file.ReadInt32E();
            LowBounds = new float3(file);
            HighBounds = new float3(file);

            Coefficients = new Pointer32_float3s(file, file.ReadUInt32E());
            SegmentLengths = new Pointer32_f(file, (ushort)file.ReadUInt32E());
            ParamValues = new Pointer32_b(file, (ushort)file.ReadUInt32E());
        
            if (_ParamValues.Count != 0) throw new NotImplementedException();
        }

        public override void Update(HoArchive.TOCEntry entry){
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(ClosedCurve);
            LowBounds.Save(file);
            HighBounds.Save(file);

            file.WriteE(_Coefficients.Count);
            Coefficients.SavePointer(file);
            file.WriteE(_SegmentLengths.Count);
            SegmentLengths.SavePointer(file);
            file.WriteE(_ParamValues.Count);
            ParamValues.SavePointer(file);

            Coefficients.Save(file);
            SegmentLengths.Save(file);
            ParamValues.Save(file);
        }
    }
}