
namespace Asset{
    public static class AssetCaster{

        public static AssetEntity ReadAsset(HoArchive.BinaryReaderEndian file, uint elementOffset, uint blobSize, uint wmlTypeID, string target){
            switch (target){
                case "SB09":
                    return ReadAssetSB09(file, elementOffset, blobSize, wmlTypeID, new AssetKeySB09());
                
                default:
                    return null;
            }
        }

        public static AssetEntity ReadAssetSB09(HoArchive.BinaryReaderEndian file, uint elementOffset, uint blobSize, uint wmlTypeID, AssetKeySB09 AssetKey){
            if (!AssetKey.Contains(wmlTypeID)){
                return null;
            }
            string wmlTypeName = AssetKey.key[wmlTypeID];

            switch (wmlTypeName){
                case "SimpleObject":
                    return new SB09Assets.SimpleObject(file, elementOffset, blobSize);

                default:
                    return null;
            }
        }
    }
}