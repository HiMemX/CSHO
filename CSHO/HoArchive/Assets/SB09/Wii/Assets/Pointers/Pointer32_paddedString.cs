namespace SB09WiiAsset{
    public class Pointer32_paddedString : Pointer32{
        public string str {get; set;}

        public Pointer32_paddedString(string str){
            this.str = str;
        }

        public Pointer32_paddedString(HoArchive.MemoryStreamEndian file) : base(file){
            file.Jump(_p);
            str = file.ReadUntil(0);
            file.Return();
        }

        public void Update(HoArchive.TOCEntry entry){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteString(str+"\0");
            file.PadAlign(0x04, 0x00);
        }
    }
}