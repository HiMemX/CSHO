using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Model : Asset.AssetEntity{
        public ushort geomCount;
        public ushort rendCount;
        public ushort childTransformCount;
        public ushort refModelInstanceCount;
        public ushort modelPartCount;
        public byte segmentedModel;
        public bool shadowRotate;
        public ushort lightMask;
        public byte sectorPointCount;
        public byte sectorSpotCount;
        public uint instanceParamCount;
        public uint shadowPriority;
        public uint shadowColor;
        public float shadowMaxDepth;
        public float shadowStartDepth;
        public float shadowBlurDepth;
        public ushort shadowMinBlur;
        public ushort shadowMaxBlur;
        public ulong skeletonID;
        public ulong collmeshID;
        public ulong shadowTextureID;
        public float shadowTextureLength;
        public float shadowTextureWidth;
        public float shadowCalculatedLength;
        public float shadowCalculatedWidth;
        public float shadowTextureOffsetX;
        public float shadowTextureOffsetZ;
        public List<Matrix4x3> childTransforms; // Not in dwarf
        public List<ulong> geometryIDs; // Not in dwarf
        public List<ulong> childModelIDs; // Not in dwarf
        public List<ulong> unknown0; // Not in dwarf


        public Model(HoArchive.MemoryStreamEndian file){
        }

        public override void Update(HoArchive.TOCEntry entry){
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
        }
    }
}