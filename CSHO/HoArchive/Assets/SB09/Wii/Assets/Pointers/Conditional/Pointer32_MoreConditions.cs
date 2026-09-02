using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using HoArchive;

namespace SB09WiiAsset{
    public class Pointer32_MoreConditions : Pointer32{
        public List<ConditionDef> MoreConditions {get; set;}

        public Pointer32_MoreConditions(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            
            MoreConditions = new List<ConditionDef>();
            file.Jump(_p);
            for(int i=0; i<count; i++){
                MoreConditions.Add(new ConditionDef(file));
            }
            file.Return();
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
            base.Save(file);
            foreach (ConditionDef param in MoreConditions)
            {
                param.Save(file);
            }
        }
    }
}