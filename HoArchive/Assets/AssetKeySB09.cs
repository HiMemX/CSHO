using System.Collections.Generic;

namespace Asset{
    public class AssetKeySB09{
        public Dictionary<uint, string> key = new Dictionary<uint, string>{
            {2941084193, "SimpleObject"}
        };

        public bool Contains(uint wmlTypeID){
            return key.ContainsKey(wmlTypeID);
        }
    }
}