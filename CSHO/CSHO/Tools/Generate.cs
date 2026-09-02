using System;
using System.Collections.Generic;

namespace CSHO{
    public partial class Handler{
        public ulong GenerateAssetID(List<ulong> extraids = null){
            byte[] AssetID = new byte[8];


            while (true)
            {
                RNG.NextBytes(AssetID);

                
                if (extraids != null)
                {
                    if (extraids.Contains((ulong)BitConverter.ToInt64(AssetID, 0))) continue;
                }

                if (GetAsset((ulong)BitConverter.ToInt64(AssetID, 0)) == null) { return (ulong)BitConverter.ToInt64(AssetID, 0); }
            }
        }
    }
}