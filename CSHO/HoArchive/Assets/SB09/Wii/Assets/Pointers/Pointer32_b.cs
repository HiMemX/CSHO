using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_b : Pointer32{
        public List<byte> b {get; set;}

        public Pointer32_b(){
            b = new List<byte>();
        }

        public Pointer32_b(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            file.Jump(_p);
            b = new List<byte>();
            for(int x=0; x<count; x++){
                b.Add(file.ReadByte());
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(byte flt in b){
                file.WriteE(flt);
            }
        }
    }
}