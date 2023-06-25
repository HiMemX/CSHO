using System.ComponentModel;

namespace SB09WiiAsset{
    public class Texture : Asset.AssetEntity{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong imageBlobID {get; set;}
        public byte tileX {get; set;}
        public byte tileY {get; set;}
        public byte tileZ {get; set;}
        public byte pad {get; set;}
        public float alphaScale {get; set;}

        public Texture(){}

        public Texture(HoArchive.MemoryStreamEndian file){
            imageBlobID = file.ReadUInt64E();
            tileX = file.ReadByte();
            tileY = file.ReadByte();
            tileZ = file.ReadByte();
            pad = file.ReadByte();
            alphaScale = file.ReadFloat32E();
        }

        public override void Update(HoArchive.TOCEntry entry){
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(imageBlobID);
            file.WriteE(tileX);
            file.WriteE(tileY);
            file.WriteE(tileZ);
            file.WriteE(pad);
            file.WriteE(alphaScale);
        }
    }
}