using System.Collections.Generic;

namespace CSHO{
    public partial class Handler{
        public bool PartOfList(List<byte> lista, List<byte> listb){
            int i;
            int b;
            bool loopbroke;
            for(i=0; i<lista.Count-listb.Count+1; i++){
                loopbroke = false;
                for(b=0; b<listb.Count; b++){
                    if(lista[i+b] != listb[b]){loopbroke = true; break;}
                }
                if(!loopbroke){return true;}
            }
            return false;
        }
    }
}