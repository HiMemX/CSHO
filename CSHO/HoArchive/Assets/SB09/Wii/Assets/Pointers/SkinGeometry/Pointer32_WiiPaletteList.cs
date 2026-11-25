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
            wiiSkinPaletteList = ReadItems<WiiSkinPalette>(file, count, stream => new WiiSkinPalette(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, wiiSkinPaletteList, (stream, item) => item.Save(stream));

            foreach(WiiSkinPalette section in wiiSkinPaletteList){
                section.SaveHeap(file);
            }
        }
    }
}