namespace SB09WiiEvent;

using System.ComponentModel;
using Asset;
using HoArchive;

public class SetPlayerPosition : EventEntity{
    public uint unknown0 { get; set; }
    public uint unknown1 { get; set; }

    [TypeConverter(typeof(AssetIDConverter))]
    public ulong directionID { get; set; }

    public SetPlayerPosition(){}

    public SetPlayerPosition(HoArchive.MemoryStreamEndian file)
    {
        unknown0 = file.ReadUInt32E();
        unknown1 = file.ReadUInt32E();
        directionID = file.ReadUInt64E();
    }

    public override void Save(HoArchive.MemoryStreamEndian file)
    {
        file.WriteE(unknown0);
        file.WriteE(unknown1);
        file.WriteE(directionID);
    }
}