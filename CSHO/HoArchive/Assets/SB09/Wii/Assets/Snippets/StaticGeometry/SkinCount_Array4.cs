

namespace SB09WiiAsset{
    public class SkinCount_Array4{
        public byte element0;
        public byte element1;
        public byte element2;
        public byte element3;

        public SkinCount_Array4(){}

        public SkinCount_Array4(HoArchive.MemoryStreamEndian file){
            element0 = file.ReadByte();
            element1 = file.ReadByte();
            element2 = file.ReadByte();
            element3 = file.ReadByte();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(element0);
            file.WriteE(element1);
            file.WriteE(element2);
            file.WriteE(element3);
        }
    }
}