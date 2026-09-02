using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Streams : Pointer32{
        public List<VertexStream> streams {get;set;}

        public Pointer32_Streams(){
        }

        public Pointer32_Streams(List<VertexStream> streams){
            this.streams = streams;
        }

        public Pointer32_Streams(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            file.Jump(_p);
            streams = new();
            for(int i=0; i<count; i++){
                streams.Add(new VertexStream(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(VertexStream stream in streams){
                stream.Save(file);
            }
        }
    }
}