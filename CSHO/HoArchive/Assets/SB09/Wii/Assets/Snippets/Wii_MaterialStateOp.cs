using System.ComponentModel;

namespace SB09WiiAsset{
    public class Wii_MaterialStateOp : StateOp{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_MaterialSettings mat {get; set;}
        public byte flags {get; set;}
        public byte pad1 {get; set;}

        public Wii_MaterialStateOp(HoArchive.MemoryStreamEndian file) : base(file){
            mat = new Wii_MaterialSettings(file);
            flags = file.ReadByte();
            pad1 = file.ReadByte();
        }

        public new void Update(){
            size = 0x16;
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            mat.Save(file);
            file.WriteE(flags);
            file.WriteE(pad1);
        }
    }
}