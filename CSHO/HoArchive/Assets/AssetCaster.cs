using System.Linq;

namespace Asset{
    public static class AssetCaster{
        public static AssetEntity ReadAsset(HoArchive.MemoryStreamEndian file, HoArchive.wmlTypeID wmlTypeID, string target, string platform){
            if(!AvailableAssetsTEMP.available.Contains(wmlTypeID)){return null;}
            switch (target){
                case "SB09":
                    if(platform == "WII"){return SB09WiiAsset.SB09WiiAssetCaster.ReadAsset(file, wmlTypeID);}
                    break;
                default:
                    return null;
            }
            return null;
        }
    }
}