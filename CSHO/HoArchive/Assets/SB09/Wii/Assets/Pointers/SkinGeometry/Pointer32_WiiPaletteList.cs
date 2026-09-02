using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_WiiPaletteList : Pointer32{
        public List<WiiSkinPalette> wiiSkinPaletteList {get; set;}

        public Pointer32_WiiPaletteList(){
            wiiSkinPaletteList = new List<WiiSkinPalette>();
        }

        public Pointer32_WiiPaletteList(List<WiiSkinPalette> wiiSkinPaletteList){
            this.wiiSkinPaletteList = wiiSkinPaletteList;
        }

        public Pointer32_WiiPaletteList(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            file.Jump(_p);
            wiiSkinPaletteList = new List<WiiSkinPalette>();
            for(int x=0; x<count; x++){
                wiiSkinPaletteList.Add(new WiiSkinPalette(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(WiiSkinPalette section in wiiSkinPaletteList){
                section.Save(file);
            }

            foreach(WiiSkinPalette section in wiiSkinPaletteList){
                section.SaveHeap(file);
            }
        }
    }
}