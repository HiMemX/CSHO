using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_renderCustomizers : Pointer32{
        public List<renderCustomizer> renderCustomizers {get; set;}

        public Pointer32_renderCustomizers(HoArchive.MemoryStreamEndian file, uint renderCustomizerCount) : base(file){
            renderCustomizers = new List<renderCustomizer>();
            file.Jump(_p);
            for(int i=0; i<renderCustomizerCount; i++){
                renderCustomizers.Add(new renderCustomizer(file));
            }
            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(renderCustomizer customizer in renderCustomizers){
                customizer.Save(file);
            }
        }
    }
}