
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class VertexStream{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong vertexBufferID {get;set;} = new();
        public byte stride {get;set;} = new();
        public byte[] pad; // Ya don't need to access pads lmao

        public VertexStream(){
            pad = new byte[7];
        }

        public VertexStream(HoArchive.MemoryStreamEndian file){
            vertexBufferID = file.ReadUInt64E();
            stride = file.ReadByte();
            pad = file.ReadBytes(7);
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(vertexBufferID);
            file.WriteE(stride);
            file.WriteE(pad);
        }
    }
}