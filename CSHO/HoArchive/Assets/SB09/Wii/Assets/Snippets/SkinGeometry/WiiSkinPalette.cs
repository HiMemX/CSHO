using System.Collections.Generic;
using HoArchive;

namespace SB09WiiAsset{
    public class WiiSkinPalette{
        public ushort displayListSize {get;set;}
        public ushort matCount {get;set;}
        public Pointer32_MatList matList;
        public List<Mat> _matList  {get {return matList.matList;} set {matList.matList = value;}}
    
        public WiiSkinPalette(MemoryStreamEndian file){
            displayListSize = file.ReadUInt16E();
            matCount = file.ReadUInt16E();
            matList = new Pointer32_MatList(file, matCount);
        }

        public void Save(MemoryStreamEndian file){
            file.WriteE(displayListSize);
            file.WriteE(matCount);
            matList.SavePointer(file);
        }

        public void SaveHeap(MemoryStreamEndian file){
            matList.Save(file);
        }
    }
}