using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class ScaleformTexture{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong ID {get; set;}
        public string name {get; set;}

        public ScaleformTexture(ulong textureID, string textureName){
            this.ID = textureID;
            this.name = textureName;
        }

        public ScaleformTexture(){
            name = "";
        }
    }
}