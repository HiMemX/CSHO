using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset;

public class __MoreConditions__
{
    public uint count { get; set; }
    public uint data { get; set; }


    public __MoreConditions__(MemoryStreamEndian file)
    {
        count = file.ReadUInt32E();
        data = file.ReadUInt32E();
    }

    public void Save(MemoryStreamEndian file)
    {
        file.WriteE(count);
        file.WriteE(data);
    }
}