using System.ComponentModel;

namespace SB09WiiAsset{
    public class renderCustomizer{
        public uint unknown0 {get; set;}
        public uint unknown1 {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong unknown2 {get; set;}

        public renderCustomizer(){}

        public renderCustomizer(HoArchive.MemoryStreamEndian file){
            unknown0 = file.ReadUInt32E();
            unknown1 = file.ReadUInt32E();
            unknown2 = file.ReadUInt64E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(unknown0);
            file.WriteE(unknown1);
            file.WriteE(unknown2);
        }
    }
}