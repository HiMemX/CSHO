using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_renderCustomizers : Pointer32{
        public List<renderCustomizer> renderCustomizers {get; set;}

        public Pointer32_renderCustomizers() : base(){
            renderCustomizers = new();
        }

        public Pointer32_renderCustomizers(HoArchive.MemoryStreamEndian file, uint renderCustomizerCount) : base(file){
            renderCustomizers = ReadItems<renderCustomizer>(file, renderCustomizerCount, stream => new renderCustomizer(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, renderCustomizers, (stream, item) => item.Save(stream));
        }
    }
}