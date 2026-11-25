using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Streams : Pointer32{
        public List<VertexStream> streams {get;set;}

        public Pointer32_Streams(){
            streams = new();
        }

        public Pointer32_Streams(List<VertexStream> streams){
            this.streams = streams;
        }

        public Pointer32_Streams(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            streams = ReadItems<VertexStream>(file, count, stream => new VertexStream(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, streams, (stream, item) => item.Save(stream));
        }
    }
}