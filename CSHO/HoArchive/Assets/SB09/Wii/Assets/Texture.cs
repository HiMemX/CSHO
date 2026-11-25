using System.ComponentModel;

namespace SB09WiiAsset{
    public class Texture : Asset.AssetEntity{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong imageBlobID {get; set;} = new();
        public byte tileX {get; set;} = new();
        public byte tileY {get; set;} = new();
        public byte tileZ {get; set;} = new();
        public byte pad {get; set;} = new();
        public float alphaScale {get; set;} = new();

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
            base.Update(entry);
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(imageBlobID);
            file.WriteE(tileX);
            file.WriteE(tileY);
            file.WriteE(tileZ);
            file.WriteE(pad);
            file.WriteE(alphaScale);
        }

        public ulong GetTextureBufferID()
        {
            return imageBlobID;
        }
    }
}