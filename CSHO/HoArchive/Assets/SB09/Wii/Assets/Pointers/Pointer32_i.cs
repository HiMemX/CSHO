using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_i : Pointer32{
        public List<uint> i {get; set;}

        public Pointer32_i(){
            i = new List<uint>();
        }

        public Pointer32_i(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            i = ReadItems<uint>(file, count, stream => stream.ReadUInt32E());
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, i, (stream, item) => stream.WriteE(item));
        }
    }
}