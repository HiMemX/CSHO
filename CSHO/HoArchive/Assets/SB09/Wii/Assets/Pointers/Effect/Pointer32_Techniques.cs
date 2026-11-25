using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Techniques : Pointer32{
        public List<Technique> techniques {get; set;}

        public Pointer32_Techniques() : base(){
            techniques = new();
        }

        public Pointer32_Techniques(HoArchive.MemoryStreamEndian file) : base(file){
            byte count = file.ReadByte();
            techniques = ReadItems<Technique>(file, count, stream => new Technique(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, techniques, (stream, item) => item.Save(stream));
        }
    }
}