
namespace HoArchive{
    public class TableEntry{
        public string sectionType;
        public ushort packLangID;
        public byte parcelType;
        public byte pad;
        public uint userKey;
        public uint nameHash;
        public uint namePtr;
        public uint fromNameHash;
        public uint fromNamePtr;
        public uint startSector;
        public uint sizeOnDisk;
        public uint sizeInMem;
        public uint memoryAlignment;
        public uint attributeFlags;
        public uint externName;
        public uint metaBlockCount;
        public uint metaRecord;
        public uint reserved;
        public TableEntry(BinaryReaderEndian file){ // Updating gets handeled by Table.cs
            sectionType     = file.ReadStringE(0x04); // Update
            packLangID      = file.ReadUInt16E();
            parcelType      = file.ReadByte();
            pad             = file.ReadByte();
            userKey         = file.ReadUInt32E();
            nameHash        = file.ReadUInt32E();
            namePtr         = file.ReadUInt32E();
            fromNameHash    = file.ReadUInt32E();
            fromNamePtr     = file.ReadUInt32E();
            startSector     = file.ReadUInt32E(); // Update (Although later)
            sizeOnDisk      = file.ReadUInt32E(); // Update
            sizeInMem       = file.ReadUInt32E(); // Update
            memoryAlignment = file.ReadUInt32E();
            attributeFlags  = file.ReadUInt32E();
            externName      = file.ReadUInt32E();
            metaBlockCount  = file.ReadUInt32E(); // Update
            metaRecord      = file.ReadUInt32E(); // Update
            reserved        = file.ReadUInt32E();
        }

        public void Save(BinaryWriterEndian file){
            file.WriteStringE(sectionType);
            file.WriteE(packLangID);
            file.WriteE(parcelType);
            file.WriteE(pad);
            file.WriteE(userKey);
            file.WriteE(nameHash);
            file.WriteE(namePtr);
            file.WriteE(fromNameHash);
            file.WriteE(fromNamePtr);
            file.WriteE(startSector);
            file.WriteE(sizeOnDisk);
            file.WriteE(sizeInMem);
            file.WriteE(memoryAlignment);
            file.WriteE(attributeFlags);
            file.WriteE(externName);
            file.WriteE(metaBlockCount);
            file.WriteE(metaRecord);
            file.WriteE(reserved);
        }

        
    }
}