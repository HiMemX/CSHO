using System.ComponentModel;

namespace SB09WiiAsset{
    public class xBaseAsset : Asset.AssetEntity{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong id {get; set;}
        [TypeConverter(typeof(HoArchive.wmlTypeIDConverter))]
        public HoArchive.wmlTypeID baseType {get; set;}
        public ushort linkCount {get; set;}
        public ushort baseFlags {get; set;}

        public xBaseAsset(HoArchive.MemoryStreamEndian file){
            id = file.ReadUInt64E();
            baseType = (HoArchive.wmlTypeID)file.ReadUInt32E();
            linkCount = file.ReadUInt16E();
            baseFlags = file.ReadUInt16E();
        }

        public override void Update(HoArchive.TOCEntry entry){
            id = entry.uidSelf;
            baseType = entry.wmlTypeID;
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(id);
            file.WriteE((uint)baseType);
            file.WriteE(linkCount);
            file.WriteE(baseFlags);
        }
    }
}