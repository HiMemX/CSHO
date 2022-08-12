using System.Collections.Generic;

class TOCEntry{
    public uint elementSize;
    public uint elementOffset;
    public uint blobSize;
    public uint blobAlign;
    public byte[] uidSelf = new byte[8];
    public byte[] wmlTypeID = new byte[4];
    public ushort subType;
    public ushort blobFlags;

    public List<byte> data = new List<byte>();
    public TOCEntry(BinaryReaderEndian file, uint DataPtr){
        elementSize   = file.ReadUInt32E();
        elementOffset = file.ReadUInt32E();
        blobSize      = file.ReadUInt32E();
        blobAlign     = file.ReadUInt32E();
        uidSelf       = file.ReadBytesE(8);
        wmlTypeID     = file.ReadBytesE(4);
        subType       = file.ReadUInt16E();
        blobFlags     = file.ReadUInt16E();

        uint returnaddr = (uint)file.BaseStream.Position;
        file.BaseStream.Position = DataPtr;
        data = new List<byte>(file.ReadBytes((int)blobSize));
        file.BaseStream.Position = returnaddr;
    }

    public void Update(uint Align){
        elementSize   = MathTools.RoundUpTo((uint)data.Count, Align);
        blobSize      = (uint)data.Count;
    }

    public void SaveData(BinaryWriterEndian file){
        file.Write(data.ToArray());
        file.Pad(elementSize - blobSize, 0x33);
    }

    public void SaveMeta(BinaryWriterEndian file){
        file.WriteE(elementSize);
        file.WriteE(elementOffset);
        file.WriteE(blobSize);
        file.WriteE(blobAlign);
        file.WriteE(uidSelf);
        file.WriteE(wmlTypeID);
        file.WriteE(subType);
        file.WriteE(blobFlags);
    }
}