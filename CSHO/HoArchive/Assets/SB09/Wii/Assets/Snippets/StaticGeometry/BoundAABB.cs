
using System.ComponentModel;
using System.Numerics;
using HoArchive;

namespace SB09WiiAsset{
    public class BoundAABB{
        [TypeConverter(typeof(Point3Converter))]
        public HoArchive.float3 lower{get;set;}
        [TypeConverter(typeof(Point3Converter))]
        public HoArchive.float3 upper{get;set;}
        
        public BoundAABB(HoArchive.MemoryStreamEndian file){
            lower = new HoArchive.float3(file);
            upper = new HoArchive.float3(file);
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            lower.Save(file);
            upper.Save(file);
        }
    }
}