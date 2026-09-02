using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset;

public class TriggerCylinder : TriggerSubtype{
    [TypeConverter(typeof(Point3Converter))]
    public float3 Center {get;set;}
    public float Radius {get;set;}
    public float Height {get;set;}

    public TriggerCylinder(){
        Center = new float3();
        Radius = 1;
        Height = 1;
    }

    public TriggerCylinder(MemoryStreamEndian file){
        Center = new float3(file);
        Radius = file.ReadFloat32E();
        Height = file.ReadFloat32E();
    }

    public override void Save(MemoryStreamEndian file){
        Center.Save(file);
        file.WriteE(Radius);
        file.WriteE(Height);
    }
}