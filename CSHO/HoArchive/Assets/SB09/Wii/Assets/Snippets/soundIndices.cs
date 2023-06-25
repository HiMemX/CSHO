using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class soundIndices{
        public uint pathlength {get; set;}
        public List<soundIndex> path {get; set;}

        public soundIndices(){
            path = new List<soundIndex>();
            pathlength = 0;
        }        

        public soundIndices(HoArchive.MemoryStreamEndian file){
            path = new List<soundIndex>();
            pathlength = file.ReadUInt32E();
            for(int i=0; i<pathlength; i++){
                path.Add(new soundIndex(file));
            }
        }

        public void Update(){
            pathlength = (uint)path.Count;
            foreach(soundIndex index in path){index.Update();}
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(pathlength);
            foreach(soundIndex index in path){
                index.Save(file);
            }
        }
    }
}