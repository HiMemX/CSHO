

namespace SB09WiiAsset{
    public class Pointer32_MaterialParamFormatTableAsset : Pointer32{
        public byte count {get; set;} = new();
        public byte pad {get; set;} = new();
        public ushort dataSize {get; set;} = new();

        public Pointer32_MaterialParamFormatTableAsset() : base(){}

        public Pointer32_MaterialParamFormatTableAsset(HoArchive.MemoryStreamEndian file) : base(file){
            count = file.ReadByte();
            pad = file.ReadByte();
            dataSize = file.ReadUInt16E();

            if(count != 0){ throw new System.NotImplementedException();}

            file.Jump(_p);
            file.Return();
        }

        public new void Update(){
            count = 0;
            dataSize = 0;
        }

        public new void SavePointer(HoArchive.MemoryStreamEndian file){
            base.SavePointer(file);
            file.WriteE(count);
            file.WriteE(pad);
            file.WriteE(dataSize);
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
        }
    }
}