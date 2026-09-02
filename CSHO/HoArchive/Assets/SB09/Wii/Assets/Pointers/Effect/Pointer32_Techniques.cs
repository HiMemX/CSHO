using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Techniques : Pointer32{
        public List<Technique> techniques {get; set;}

        public Pointer32_Techniques(HoArchive.MemoryStreamEndian file) : base(file){
            byte count = file.ReadByte();
            file.Jump(_p);
            techniques = new List<Technique>();
            for(int x=0; x<count; x++){
                techniques.Add(new Technique(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(Technique tech in techniques){
                tech.Save(file);
            }
        }
    }
}