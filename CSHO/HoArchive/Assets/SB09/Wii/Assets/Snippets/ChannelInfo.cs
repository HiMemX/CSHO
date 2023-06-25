using System.ComponentModel;

namespace SB09WiiAsset{
    public class ChannelInfo{
        public byte vtype {get; set;}
        public byte vfrac {get; set;}
        public byte vindex {get; set;}
        public byte vcmp {get; set;}
        public byte ifmt {get; set;}
        public byte voffset {get; set;}
        public byte vstride {get; set;}
        public byte pad {get; set;}

        public ChannelInfo(HoArchive.MemoryStreamEndian file){
            vtype = file.ReadByte();
            vfrac = file.ReadByte();
            vindex = file.ReadByte();
            vcmp = file.ReadByte();
            ifmt = file.ReadByte();
            voffset = file.ReadByte();
            vstride = file.ReadByte();
            pad = file.ReadByte();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(vtype);
            file.WriteE(vfrac);
            file.WriteE(vindex);
            file.WriteE(vcmp);
            file.WriteE(ifmt);
            file.WriteE(voffset);
            file.WriteE(vstride);
            file.WriteE(pad);
        }
    }
}