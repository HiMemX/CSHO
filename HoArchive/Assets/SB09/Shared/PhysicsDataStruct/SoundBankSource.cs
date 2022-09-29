namespace SB09Assets{
    public class SoundBankSource{
        public byte streamed;
        public byte pad1;
        public byte pad2;
        public byte pad3;
        public uint sourceString;
        public uint indices;

        public SoundBankSource(HoArchive.BinaryReaderEndian file){
            streamed = file.ReadByte();
            pad1 = file.ReadByte();
            pad2 = file.ReadByte();
            pad3 = file.ReadByte();
            sourceString = file.ReadUInt32E();
            indices = file.ReadUInt32E();
        }
    }
}