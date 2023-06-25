using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_Wii_GeometryStateOp : Pointer32{
        public uint stateSize {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_GeometryStateOp geometryStateOp {get; set;}
        

        public Pointer32_Wii_GeometryStateOp(HoArchive.MemoryStreamEndian file) : base(file){
            stateSize = file.ReadUInt32E();
            file.Jump(_p);

            geometryStateOp = new Wii_GeometryStateOp(file);

            file.Return();
        }

        public new void Update(){
            geometryStateOp.Update();
            stateSize = 0x48;
        }

        public new void SavePointer(HoArchive.MemoryStreamEndian file){
            base.SavePointer(file);
            file.WriteE(stateSize);
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            geometryStateOp.Save(file);
        }
    }
}