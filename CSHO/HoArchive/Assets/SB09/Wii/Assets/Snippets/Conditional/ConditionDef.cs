using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset;

public class ConditionDef
{
    [TypeConverter(typeof(AssetIDConverter))]
    public ulong value_asset { get; set; }
    public uint constNum { get; set; }
    public uint expr1 { get; set; }
    public uint op { get; set; }

    public ConditionDef(MemoryStreamEndian file)
    {
        value_asset = file.ReadUInt64E();
        constNum = file.ReadUInt32E();
        expr1 = file.ReadUInt32E();
        op = file.ReadUInt32E();
    }

    public void Save(MemoryStreamEndian file)
    {
        file.WriteE(value_asset);
        file.WriteE(constNum);
        file.WriteE(expr1);
        file.WriteE(op);
    }
}