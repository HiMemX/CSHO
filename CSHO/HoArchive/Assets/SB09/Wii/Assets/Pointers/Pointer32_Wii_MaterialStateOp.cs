using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_Wii_MaterialStateOp : Pointer32{
        public uint stateSize {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_MaterialStateOp materialStateOp {get; set;}
        

        public Pointer32_Wii_MaterialStateOp(HoArchive.MemoryStreamEndian file) : base(file){
            stateSize = file.ReadUInt32E();
            file.Jump(_p);

            materialStateOp = new Wii_MaterialStateOp(file);

            file.Return();
        }

        public new void Update(){
            materialStateOp.Update();
            stateSize = 0x16;
        }

        public new void SavePointer(HoArchive.MemoryStreamEndian file){
            base.SavePointer(file);
            file.WriteE(stateSize);
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            materialStateOp.Save(file);
        }
    }
}