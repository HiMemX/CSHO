namespace SB09WiiAsset{
    public class SoundBankWrap : Asset.AssetEntity{
        public uint length {get; set;}
        public string str {get; set;}

        public SoundBankWrap(HoArchive.MemoryStreamEndian file){
            length = file.ReadUInt32E();
            str = file.ReadString((int)length-1);
        }

        public override void Update(HoArchive.TOCEntry entry){
            length = (uint)str.Length +1;
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(length);
            file.WriteString(str + "\0");
        }
    }
}