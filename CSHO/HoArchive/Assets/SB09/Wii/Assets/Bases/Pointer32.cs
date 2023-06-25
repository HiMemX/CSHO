namespace SB09WiiAsset{
    public class Pointer32{
        public uint _p;

        public Pointer32(){
            _p = 0;
        }

        public Pointer32(HoArchive.MemoryStreamEndian file){
            _p = file.ReadUInt32E();
        }

        public void Update(){
        }

        public void SavePointer(HoArchive.MemoryStreamEndian file){
            _p = (uint)file.Position;
            file.WriteE((uint)0);
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            uint offset = (uint)file.Position;
            file.Jump(_p);
            file.WriteE(offset);
            file.Return();
            _p = offset;
        }
    }
}