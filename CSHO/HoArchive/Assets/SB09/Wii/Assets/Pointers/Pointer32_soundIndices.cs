using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_soundIndices : Pointer32{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public soundIndices indices {get; set;}

        public Pointer32_soundIndices(soundIndices indices){
            this.indices = indices;
        }

        public Pointer32_soundIndices(HoArchive.MemoryStreamEndian file) : base(file){
            file.Jump(_p);
            indices = new soundIndices(file);
            file.Return();
        }

        public new void Update(){
            indices.Update();
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            indices.Save(file);
        }
    }
}