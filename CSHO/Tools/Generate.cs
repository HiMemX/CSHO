namespace CSHO{
    public partial class Handler{
        public byte[] GenerateAssetID(){
            byte[] AssetID = new byte[8];
            
            while (true){
                RNG.NextBytes(AssetID); 
                if (GetAsset(AssetID) == null){return AssetID;}
            }
        }
    }
}