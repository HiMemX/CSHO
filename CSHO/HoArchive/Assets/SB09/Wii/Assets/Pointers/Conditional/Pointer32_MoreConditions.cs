using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using HoArchive;

namespace SB09WiiAsset{
    public class Pointer32_MoreConditions : Pointer32{
        public List<ConditionDef> MoreConditions {get; set;}

        public Pointer32_MoreConditions() : base(){
            MoreConditions = new();
        }

        public Pointer32_MoreConditions(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            
            MoreConditions = ReadItems<ConditionDef>(file, count, stream => new ConditionDef(stream));
        }

        public new void Update(){
        }

        public new void SavePointer(MemoryStreamEndian file)
        {
            file.WriteE(MoreConditions.Count);
            base.SavePointer(file);
        }

        public new void Save(HoArchive.MemoryStreamEndian file)
        {
            SaveItems(file, MoreConditions, (stream, item) => item.Save(stream));
        }
    }
}