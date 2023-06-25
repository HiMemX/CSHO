using System.ComponentModel;

namespace SB09WiiAsset{
    public class Wii_GeometryStateOp : StateOp{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Wii_MaterialSettings mat {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ChannelInfo pos {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ChannelInfo norm {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ChannelInfo color {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ChannelInfo[] uv {get; set;}
        public byte posMtx {get; set;}
        public byte texMtxCount {get; set;}
        public byte pad1 {get; set;}


        public Wii_GeometryStateOp(HoArchive.MemoryStreamEndian file) : base(file){
            mat = new Wii_MaterialSettings(file);
            pos = new ChannelInfo(file);
            norm = new ChannelInfo(file);
            color = new ChannelInfo(file);
            uv = new ChannelInfo[3];
            for(int i=0; i<3; i++){uv[i] = new ChannelInfo(file);}
            posMtx = file.ReadByte();
            texMtxCount = file.ReadByte();
            pad1 = file.ReadByte();
        }

        public new void Update(){
            size = 0x48;
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            mat.Save(file);
            pos.Save(file);
            norm.Save(file);
            color.Save(file);
            for(int i=0; i<3; i++){uv[i].Save(file);}
            file.WriteE(posMtx);
            file.WriteE(texMtxCount);
            file.WriteE(pad1);
        }
    }
}