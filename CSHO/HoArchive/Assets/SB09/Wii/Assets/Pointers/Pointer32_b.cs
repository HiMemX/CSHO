using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_b : Pointer32{
        public List<byte> b {get; set;}

        public Pointer32_b(){
            b = new List<byte>();
        }

        public Pointer32_b(List<byte> b){
            this.b = b;
        }

        public Pointer32_b(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            b = ReadItems<byte>(file, count, stream => stream.ReadByte());
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, b, (stream, item) => stream.WriteE(item));
        }
    }
}