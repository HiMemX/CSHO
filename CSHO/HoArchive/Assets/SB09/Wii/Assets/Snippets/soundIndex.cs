namespace SB09WiiAsset{
    public class soundIndex{
        public bool isfile {get; set;} // 0x01 is yes
        public byte pad0 {get; set;}
        public bool isdirectory {get; set;} // 0x80 if yes
        public byte index {get; set;}

        public soundIndex(){}

        public soundIndex(bool isfile, byte index){
            this.isfile = isfile;
            pad0 = 0;
            isdirectory = isfile == false;
            this.index = index;
        }

        public soundIndex(HoArchive.MemoryStreamEndian file){
            isfile = file.ReadBool();
            pad0 = file.ReadByte();
            isdirectory = file.ReadBool();
            index = file.ReadByte();
        }

        public void Update(){
            if(isfile){isdirectory = false;}
            else{isdirectory = true;}
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(isfile);
            file.WriteE(pad0);
            if(isdirectory){file.WriteByte(0x80);}
            else{file.WriteByte(0x00); }
            file.WriteE(index);
        }
    }
}