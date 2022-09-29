using System.Collections.Generic;


namespace HoArchive{
    public class TOCEntry{
        public uint elementSize;
        public uint elementOffset;
        public uint blobSize;
        public uint blobAlign;
        public ulong uidSelf;
        public uint wmlTypeID;
        public ushort subType;
        public ushort blobFlags;

        public Asset.AssetEntity entity;

        public List<byte> data = new List<byte>();
        public TOCEntry(BinaryReaderEndian file, uint DataPtr, string target){
            elementSize   = file.ReadUInt32E();
            elementOffset = file.ReadUInt32E();
            blobSize      = file.ReadUInt32E();
            blobAlign     = file.ReadUInt32E();
            uidSelf       = file.ReadUInt64E();
            wmlTypeID     = file.ReadUInt32E();
            subType       = file.ReadUInt16E();
            blobFlags     = file.ReadUInt16E();

            uint returnaddr = (uint)file.BaseStream.Position;
            file.BaseStream.Position = DataPtr;
            data = new List<byte>(file.ReadBytes((int)blobSize));
            file.BaseStream.Position = DataPtr;
            entity = Asset.AssetCaster.ReadAsset(file, DataPtr, blobSize, wmlTypeID, target);

            file.BaseStream.Position = returnaddr;
        }

        public void Update(uint Align){
            elementSize   = MathTools.RoundUpTo((uint)data.Count, Align);
            blobSize      = (uint)data.Count;

        }

        public void SaveData(BinaryWriterEndian file){
            if (entity == null){file.Write(data.ToArray());}
            else{entity.Save(file);}
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
}