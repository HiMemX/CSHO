using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using CSHO;
using HoArchive;

namespace SB09WiiAsset{
    public class Model : Asset.AssetEntity{ // TODO: Implement shit
        public ushort geomCount {get; set;}
        public ushort rendCount {get; set;} // Is higher than geomCount sometimes! (SBB1 for example) // Future Mem here, next time be even less specific please!
        public ushort childTransformCount {get; set;}
        public ushort refModelInstanceCount {get; set;}
        public ushort modelPartCount {get; set;}
        public byte segmentedModel {get; set;}
        public bool shadowRotate {get; set;}
        public ushort lightMask {get; set;}
        public byte sectorPointCount {get; set;}
        public byte sectorSpotCount {get; set;}
        public uint instanceParamCount {get; set;}
        public uint shadowPriority {get; set;}
        public uint shadowColor {get; set;}
        public float shadowMaxDepth {get; set;}
        public float shadowStartDepth {get; set;}
        public float shadowBlurDepth {get; set;}
        public ushort shadowMinBlur {get; set;}
        public ushort shadowMaxBlur {get; set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong skeletonID {get; set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong collmeshID {get; set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong shadowTextureID {get; set;}
        public float shadowTextureLength {get; set;}
        public float shadowTextureWidth {get; set;}
        public float shadowCalculatedLength {get; set;}
        public float shadowCalculatedWidth {get; set;}
        public float shadowTextureOffsetX {get; set;}
        public float shadowTextureOffsetZ {get; set;}
        public List<Matrix4x3> transformationMatrices {get; set;} // Not in dwarf
        public List<ulong> geometryIDs {get; set;} // Not in dwarf
        public List<ulong> refModelIDs {get; set;} // Not in dwarf
        public List<instanceParamHandle> instanceParameterHandles {get; set;} // Not in dwarf
        public List<ulong> unknown0 {get; set;} // length: instanceParamCount
        public List<ushort> unknown1 {get; set;} // length: rendCount // NOTE RendCount is accuracte! GeomCount as it says in the image is wrong
        public List<ushort> matrixStack {get; set;} // length: childTransformCount
        public List<ushort> stackIndicesRend {get; set;} // length: rendCount (Idk how...)
        public List<ushort> stackIndicesRefModel {get; set;} // length: refModelCount
        public List<ushort> unknown3 {get; set;} // length: refModelCount
        // 8 bytes with rendCount for some reason
        public List<byte> unknown4 {get; set;} // Use as general end of file buffer for now //length: refModelCount


        public Model(HoArchive.MemoryStreamEndian file){
            geomCount = file.ReadUInt16E();
            rendCount = file.ReadUInt16E();
            childTransformCount = file.ReadUInt16E();
            refModelInstanceCount = file.ReadUInt16E();
            modelPartCount = file.ReadUInt16E();
            segmentedModel = file.ReadByte();
            shadowRotate = file.ReadBool();
            lightMask = file.ReadUInt16E();
            sectorPointCount = file.ReadByte();
            sectorSpotCount = file.ReadByte();
            instanceParamCount = file.ReadUInt32E();
            shadowPriority = file.ReadUInt32E();
            shadowColor = file.ReadUInt32E();
            shadowMaxDepth = file.ReadFloat32E();
            shadowStartDepth = file.ReadFloat32E();
            shadowBlurDepth = file.ReadFloat32E();
            shadowMinBlur = file.ReadUInt16E();
            shadowMaxBlur = file.ReadUInt16E();
            file.ReadUInt32E();
            skeletonID = file.ReadUInt64E();
            collmeshID = file.ReadUInt64E();
            shadowTextureID = file.ReadUInt64E();
            shadowTextureLength = file.ReadFloat32E();
            shadowTextureWidth = file.ReadFloat32E();
            shadowCalculatedLength = file.ReadFloat32E();
            shadowCalculatedWidth = file.ReadFloat32E();
            shadowTextureOffsetX = file.ReadFloat32E();
            shadowTextureOffsetZ = file.ReadFloat32E();

            // Not in dwarf
            transformationMatrices = new List<Matrix4x3>();
            for(int i=0; i<childTransformCount; i++){
                transformationMatrices.Add(new Matrix4x3(file));
            }

            geometryIDs = new List<ulong>();
            for(int i=0; i<geomCount; i++){
                geometryIDs.Add(file.ReadUInt64E());
            }

            refModelIDs = new List<ulong>();
            for(int i=0; i<refModelInstanceCount; i++){
                refModelIDs.Add(file.ReadUInt64E());
            }

            instanceParameterHandles = new List<instanceParamHandle>();
            for(int i=0; i<instanceParamCount; i++){
                instanceParameterHandles.Add(new instanceParamHandle(file));
            }

            unknown0 = new List<ulong>();
            for(int i=0; i<instanceParamCount; i++){
                unknown0.Add(file.ReadUInt64E());
            }

            unknown1 = new List<ushort>();
            for(int i=0; i<rendCount; i++){
                unknown1.Add(file.ReadUInt16E());
            }

            matrixStack = new List<ushort>();
            for(int i=0; i<childTransformCount; i++){
                matrixStack.Add(file.ReadUInt16E());
            }

            stackIndicesRend = new List<ushort>();
            for(int i=0; i<rendCount; i++){
                stackIndicesRend.Add(file.ReadUInt16E());
            }

            stackIndicesRefModel = new List<ushort>();
            for(int i=0; i<refModelInstanceCount; i++){
                stackIndicesRefModel.Add(file.ReadUInt16E());
            }

            unknown3 = new List<ushort>();
            for(int i=0; i<refModelInstanceCount; i++){
                unknown3.Add(file.ReadUInt16E());
            }

            //file.ReadUInt64E(); // Random 8 byte rendcount

            unknown4 = file.ReadBytes((int)(file.Length - file.Position)).ToList();
            
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            // The way to do updating is unclear as some counts
            // rule over multiple Lists.
            geomCount = (ushort)geometryIDs.Count;
            rendCount = (ushort)stackIndicesRend.Count;
            childTransformCount = (ushort)transformationMatrices.Count;
            refModelInstanceCount = (ushort)refModelIDs.Count;
            instanceParamCount = (ushort)instanceParameterHandles.Count;
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(geomCount);
            file.WriteE(rendCount);
            file.WriteE(childTransformCount);
            file.WriteE(refModelInstanceCount);
            file.WriteE(modelPartCount);
            file.WriteE(segmentedModel);
            file.WriteE(shadowRotate);
            file.WriteE(lightMask);
            file.WriteE(sectorPointCount);
            file.WriteE(sectorSpotCount);
            file.WriteE(instanceParamCount);
            file.WriteE(shadowPriority);
            file.WriteE(shadowColor);
            file.WriteE(shadowMaxDepth);
            file.WriteE(shadowStartDepth);
            file.WriteE(shadowBlurDepth);
            file.WriteE(shadowMinBlur);
            file.WriteE(shadowMaxBlur);
            file.Pad(4, 0);
            file.WriteE(skeletonID);
            file.WriteE(collmeshID);
            file.WriteE(shadowTextureID);
            file.WriteE(shadowTextureLength);
            file.WriteE(shadowTextureWidth);
            file.WriteE(shadowCalculatedLength);
            file.WriteE(shadowCalculatedWidth);
            file.WriteE(shadowTextureOffsetX);
            file.WriteE(shadowTextureOffsetZ);

            foreach(Matrix4x3 mat in transformationMatrices){
                mat.Save(file);
            }        

            foreach(ulong id in geometryIDs){
                file.WriteE(id);
            }

            foreach(ulong id in refModelIDs){
                file.WriteE(id);
            }

            foreach(instanceParamHandle handle in instanceParameterHandles){
                handle.Save(file);
            }

            foreach(ulong u in unknown0){
                file.WriteE(u);
            }

            foreach(ushort u in unknown1){
                file.WriteE(u);
            }

            foreach(ushort ms in matrixStack){
                file.WriteE(ms);
            }

            foreach(ushort i in stackIndicesRend){
                file.WriteE(i);
            }

            foreach(ushort u in stackIndicesRefModel){
                file.WriteE(u);
            }

            foreach(ushort u in unknown3){
                file.WriteE(u);
            }

            //file.WriteE((ulong)rendCount);
            foreach(byte u in unknown4){
                file.WriteE(u);
            }
        }

        public List<ulong> GetGeometryIDs(){
            return new List<ulong>(geometryIDs);
        }

        public List<ulong> GetModelIDs(){
            return new List<ulong>(refModelIDs);
        }

        public void UpdateInstanceMatrices(List<Matrix4x4> geomMatrices, List<Matrix4x4> modelInstanceMatrices)
        {
            geomMatrices.Clear();
            modelInstanceMatrices.Clear();

            Matrix4x4 mat;
            int matindex;
            foreach(int index in stackIndicesRefModel){
                mat = Matrix4x4.Identity;
                if(index == 0){
                    modelInstanceMatrices.Add(mat * Matrix4x4.Identity);
                    continue;
                }

                matindex = index -1;

                while (matindex >= 0){
                    mat = mat * transformationMatrices[matindex].GetSystemMatrix();
                    matindex = matrixStack[matindex] - 1;
                }

                modelInstanceMatrices.Add(mat);

            }
            
            foreach(int index in stackIndicesRend){
                mat = Matrix4x4.Identity;
                if(index == 0){
                    geomMatrices.Add(mat * Matrix4x4.Identity);
                    continue;
                }

                matindex = index -1;

                while (matindex >= 0){
                    mat = mat * transformationMatrices[matindex].GetSystemMatrix();
                    matindex = matrixStack[matindex] - 1;
                }

                geomMatrices.Add(mat);

            }
        }
    }
}