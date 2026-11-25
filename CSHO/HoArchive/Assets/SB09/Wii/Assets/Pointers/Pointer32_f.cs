using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_f : Pointer32{
        public List<float> f {get; set;}

        public Pointer32_f(){
            f = new List<float>();
        }

        public Pointer32_f(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            f = ReadItems<float>(file, count, stream => stream.ReadFloat32E());
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, f, (stream, item) => stream.WriteE(item));
        }
    }
}