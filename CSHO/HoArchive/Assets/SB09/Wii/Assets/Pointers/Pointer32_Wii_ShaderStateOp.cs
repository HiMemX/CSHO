using System.ComponentModel;

namespace SB09WiiAsset{
    public class Pointer32_Wii_ShaderStateOp : Pointer32{
        public uint stateSize {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_ShaderStateOp shaderStateOp {get; set;}
        

        public Pointer32_Wii_ShaderStateOp(HoArchive.MemoryStreamEndian file) : base(file){
            stateSize = file.ReadUInt32E();
            file.Jump(_p);

            shaderStateOp = new Wii_ShaderStateOp(file);

            file.Return();
        }

        public new void Update(){
            shaderStateOp.Update();
            stateSize = 0x0C;
        }

        public new void SavePointer(HoArchive.MemoryStreamEndian file){
            base.SavePointer(file);
            file.WriteE(stateSize);
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            shaderStateOp.Save(file);
        }
    }
}