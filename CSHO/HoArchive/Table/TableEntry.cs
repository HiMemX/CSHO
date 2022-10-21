using System.ComponentModel;

namespace HoArchive{
    public class TableEntry{ // Updated members get overwritten anyways so there isn't any point in setting them in the first place
        [ReadOnly(true)]
        public string sectionType {get; set;} // Necessary
        [TypeConverter(typeof(LanguageIDConverter))]
        public LanguageID packLangID {get; set;} // Optional
        [TypeConverter(typeof(enParcelTypeConverter))]
        public enParcelType parcelType {get; set;} // Optional
        public byte pad {get; set;}
        [TypeConverter(typeof(UIntConverter))]
        public uint userKey {get; set;} // Optional
        [TypeConverter(typeof(UIntConverter))]
        [ReadOnly(true)]
        public uint nameHash {get; set;}
        [TypeConverter(typeof(UIntConverter))]
        [ReadOnly(true)]
        public uint namePtr {get; set;}
        [TypeConverter(typeof(UIntConverter))]
        public uint fromNameHash {get; set;} // Only if parcelType = 3 (fromDomain)
        [TypeConverter(typeof(UIntConverter))]
        public uint fromNamePtr {get; set;} // Optional (Usually -1)
        [ReadOnly(true)]
        public uint startSector {get; set;}
        [ReadOnly(true)]
        public uint sizeOnDisk {get; set;}
        [ReadOnly(true)]
        public uint sizeInMem {get; set;}
        public uint memoryAlignment {get; set;}// Not settable, isn't fully understood yet
        [TypeConverter(typeof(UIntConverter))]
        public uint attributeFlags {get; set;} // Optional
        [TypeConverter(typeof(UIntConverter))]
        public uint externName {get; set;} // Optional
        [ReadOnly(true)]
        public uint metaBlockCount {get; set;}
        [ReadOnly(true)]
        public uint metaRecord {get; set;}
        public uint reserved {get; set;}
        public TableEntry(BinaryReaderEndian file){ // Updating gets handeled by Table.cs

            sectionType     = file.ReadStringE(0x04); // Update
            packLangID      = (LanguageID)file.ReadUInt16E(); 
            parcelType      = (enParcelType)file.ReadByte();
            pad             = file.ReadByte();
            userKey         = file.ReadUInt32E();
            nameHash        = file.ReadUInt32E(); // Update
            namePtr         = file.ReadUInt32E(); // Update
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

        public TableEntry(string sectionType_in, 
                        LanguageID packLangID_in = 0, 
                        enParcelType parcelType_in = enParcelType.PARCEL_TYPE_EXCLUSIVE, 
                        uint userKey_in = 0, 
                        uint fromNameHash_in = 0, 
                        uint fromNamePtr_in = 0xFFFFFFFF,
                        uint attributeFlags_in = 0,
                        uint externName_in = 0xFFFFFFFF){

            sectionType = sectionType_in;
            packLangID = packLangID_in;
            parcelType = parcelType_in;
            pad = 0x33;
            userKey = userKey_in;
            nameHash = 0;
            namePtr = 0;
            fromNameHash = fromNameHash_in;
            fromNamePtr = fromNamePtr_in;
            startSector = 0;
            sizeOnDisk = 0;
            sizeInMem = 0;

            if(sectionType_in != "PD  " && sectionType_in != "SECT"){memoryAlignment = 0x40;}
            else{memoryAlignment = 0xFFFFFFFF;}

            attributeFlags = attributeFlags_in;
            externName = externName_in;
            metaBlockCount = 0;
            metaRecord = 0;
            reserved = 0;
        }

        public void Save(BinaryWriterEndian file){
            file.WriteStringE(sectionType);
            file.WriteE((ushort)packLangID);
            file.WriteE((byte)parcelType);
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