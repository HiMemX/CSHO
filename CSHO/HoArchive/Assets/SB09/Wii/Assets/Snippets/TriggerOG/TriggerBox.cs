using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset;

public class TriggerBox : TriggerSubtype{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Matrix4x3 Transform {get;set;} 

    public TriggerBox(){
        Transform = new Matrix4x3();
        Transform.Identity();
    }

    public TriggerBox(MemoryStreamEndian file){
        Transform = new Matrix4x3(file);
    }

    public override void Save(MemoryStreamEndian file){
        Transform.Save(file);
    }
}