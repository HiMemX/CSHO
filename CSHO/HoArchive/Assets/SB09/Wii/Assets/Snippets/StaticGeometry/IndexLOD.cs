
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class IndexLOD{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong indexBufferID {get;set;} = new();
        public ushort indexCount {get;set;} = new();
        public ushort primCount {get;set;} = new();
        public ushort triCount {get;set;} = new();
        public ushort vertCount {get;set;} = new();
        public float lodDist {get;set;} = new();
        public byte skinSectStart {get;set;} = new();
        public byte skinSectCount {get;set;} = new();
        public ushort pad {get;set;} = new();

        public IndexLOD(){}

        public IndexLOD(HoArchive.MemoryStreamEndian file){
            indexBufferID = file.ReadUInt64E();
            indexCount = file.ReadUInt16E();
            primCount = file.ReadUInt16E();
            triCount = file.ReadUInt16E();
            vertCount = file.ReadUInt16E();
            lodDist = file.ReadFloat32E();
            skinSectStart = file.ReadByte();
            skinSectCount = file.ReadByte();
            pad = file.ReadUInt16E();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(indexBufferID);
            file.WriteE(indexCount);
            file.WriteE(primCount);
            file.WriteE(triCount);
            file.WriteE(vertCount);
            file.WriteE(lodDist);
            file.WriteE(skinSectStart);
            file.WriteE(skinSectCount);
            file.WriteE(pad);
        }
    }
}