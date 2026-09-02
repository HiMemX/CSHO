
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class IndexLOD{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong indexBufferID {get;set;}
        public ushort indexCount {get;set;}
        public ushort primCount {get;set;}
        public ushort triCount {get;set;}
        public ushort vertCount {get;set;}
        public float lodDist {get;set;}
        public byte skinSectStart {get;set;}
        public byte skinSectCount {get;set;}
        public ushort pad {get;set;}

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