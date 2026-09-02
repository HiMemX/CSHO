namespace SB09WiiAsset{
    public class Pointer32_string : Pointer32{
        public string str {get; set;}

        public Pointer32_string(string str){
            this.str = str;
        }

        public Pointer32_string(HoArchive.MemoryStreamEndian file) : base(file){
            file.Jump(_p);
            str = file.ReadUntil(0);
            file.Return();
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteString(str+"\0");
        }
    }
}