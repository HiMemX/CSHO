using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_i : Pointer32{
        public List<uint> i {get; set;}

        public Pointer32_i(){
            i = new List<uint>();
        }

        public Pointer32_i(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            file.Jump(_p);
            i = new List<uint>();
            for(int x=0; x<count; x++){
                i.Add(file.ReadUInt32E());
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(uint flt in i){
                file.WriteE(flt);
            }
        }
    }
}