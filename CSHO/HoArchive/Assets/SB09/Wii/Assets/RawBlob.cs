using System.Collections.Generic;
using System.Linq;

namespace SB09WiiAsset{
    public class RawBlob : Asset.AssetEntity{
        public List<byte> data = new();
        public RawBlob(HoArchive.MemoryStreamEndian file)
        {
            data = file.ReadBytes((int)file.Length).ToList();
        }

        public override void Update(HoArchive.TOCEntry entry){
        }

        public override void Save(HoArchive.MemoryStreamEndian file)
        {
            file.Write(data.ToArray());
        }
    }
}