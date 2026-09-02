using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset;

public class TriggerPolygon : TriggerSubtype{
    [TypeConverter(typeof(AssetIDConverter))]
    public ulong BoundsID {get;set;}

    public TriggerPolygon(){}

    public TriggerPolygon(MemoryStreamEndian file){
        BoundsID = file.ReadUInt64E();
    }

    public override void Save(MemoryStreamEndian file){
        file.WriteE(BoundsID);
    }

}