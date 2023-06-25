using System.ComponentModel;

namespace SB09WiiAsset{
    public class ShaderAsset : Asset.AssetEntity{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong vertexCodeID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong pixelCodeID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong vertexDeclID {get; set;}

        public ShaderAsset(HoArchive.MemoryStreamEndian file){
            vertexCodeID = file.ReadUInt64E();
            pixelCodeID = file.ReadUInt64E();
            vertexDeclID = file.ReadUInt64E();
        }

        public override void Update(HoArchive.TOCEntry entry){
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(vertexCodeID);
            file.WriteE(pixelCodeID);
            file.WriteE(vertexDeclID);
        }
    }
}