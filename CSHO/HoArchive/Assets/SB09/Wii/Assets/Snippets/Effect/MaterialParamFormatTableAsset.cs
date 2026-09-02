using System;
using System.ComponentModel;
using HoArchive;
using System.Collections.Generic;

namespace SB09WiiAsset{
    public class MaterialParamFormatTableAsset{
        public Pointer32_Techniques entries;
        public List<Technique> _entries {get {return entries.techniques;} set{entries.techniques = value;}}
        // Count not listed here as it's read by the techniques pointer
        public byte pad {get; set;}
        public ushort dataSize {get; set;}

        public MaterialParamFormatTableAsset(HoArchive.MemoryStreamEndian file){
            entries = new Pointer32_Techniques(file);
            pad = file.ReadByte();
            dataSize = file.ReadUInt16E();
        }

        public void Update(){
            dataSize = 0;
            for(int t=0; t<entries.techniques.Count; t++){
                dataSize += entries.techniques[t].renderStateFlags; // Hello, future Felix here. Why the fuck do the flags count towards the size?
            }
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            entries.SavePointer(file);
            file.WriteE((byte)entries.techniques.Count);
            file.WriteE(pad);
            file.WriteE(dataSize);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            entries.Save(file);
        }
    }
}