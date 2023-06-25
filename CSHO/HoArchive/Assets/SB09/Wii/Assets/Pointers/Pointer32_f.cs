using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_f : Pointer32{
        public List<float> f {get; set;}

        public Pointer32_f(){
            f = new List<float>();
        }

        public Pointer32_f(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            file.Jump(_p);
            f = new List<float>();
            for(int i=0; i<count; i++){
                f.Add(file.ReadFloat32E());
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(float flt in f){
                file.WriteE(flt);
            }
        }
    }
}