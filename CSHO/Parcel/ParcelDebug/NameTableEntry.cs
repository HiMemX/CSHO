
class NameTableEntry{
    public byte[] uidAsset = new byte[8];
    public uint nameOffset;
    public byte[] typeID = new byte[4];
    public uint[] unknown = new uint[4];
    
    public string name;

    public NameTableEntry(BinaryReaderEndian file){
        long baseposition = file.BaseStream.Position;
        uidAsset = file.ReadBytesE(8);
        nameOffset = file.ReadUInt32E();
        typeID = file.ReadBytesE(4);

        for (int i=0; i<4; i++){
            unknown[i] = file.ReadUInt32E();
        }

        file.BaseStream.Position = baseposition + nameOffset;
        name = file.ReadUntil(0);
    }

    public void Update(){
        nameOffset = 0x20;
    }

    public void Save(BinaryWriterEndian file){
        file.WriteE(uidAsset);
        file.WriteE(nameOffset);
        file.WriteE(typeID);
        foreach (uint unknwn in unknown){
            file.WriteE(unknwn);
        }
        file.WriteString(name + "\0");
        file.PadAlign(0x40, 0x33);
    }
}