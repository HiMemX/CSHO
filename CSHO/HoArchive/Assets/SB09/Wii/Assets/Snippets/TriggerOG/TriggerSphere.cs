using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset;

public class TriggerSphere : TriggerSubtype{
    [TypeConverter(typeof(Point3Converter))]
    public float3 Center {get;set;}
    public float Radius {get;set;}

    public TriggerSphere(){
        Center = new float3();
        Radius = 1;
    }

    public TriggerSphere(MemoryStreamEndian file){
        Center = new float3(file);
        Radius = file.ReadFloat32E();
    }

    public override void Save(MemoryStreamEndian file){
        Center.Save(file);
        file.WriteE(Radius);
    }
}