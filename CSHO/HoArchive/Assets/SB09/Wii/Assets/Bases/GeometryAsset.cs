using System.ComponentModel;
using System.Numerics;
using HoArchive;
using System.Collections.Generic;
using System.Linq;

namespace SB09WiiAsset{
    public class GeometryAsset : Asset.AssetEntity{
        [TypeConverter(typeof(Point4Converter))]
        public HoArchive.float4 boundSphere {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BoundAABB boundAABB {get; set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong materialID {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ViewAttrib viewAttrib {get;set;} // Single byte
        public byte flags {get;set;}
        public byte wiiIndexStride {get;set;}
        public byte wiiIndexSkin {get;set;}
        public ushort vertexCount {get;set;}
        public byte prim {get;set;} // Might be an enumerator
        public byte geomParamCount {get;set;}
        public byte rendParamCount {get;set;}
        public byte streamCount {get;set;}
        public ushort streamMapBufferCount {get;set;}
        
        // I just found out how to handle pointers better :D
        public Pointer32_MaterialParams geomParams;
        public Pointer32_MaterialParams rendParams;
        public List<MaterialParam> _geomParams {get {return geomParams.materialParams;} set {geomParams.materialParams = value;}}
        public List<MaterialParam> _rendParams {get {return rendParams.materialParams;} set {rendParams.materialParams = value;}}
        
        public Pointer32_Streams streams;
        public Pointer32_b streamMapBuffer; // Always points to 8 zero bytes? (Never used)
        public List<VertexStream> _streams {get {return streams.streams;} set{streams.streams = value;}}
        public List<byte> _streamMapBuffer {get {return streamMapBuffer.b;} set{streamMapBuffer.b = value;}}
        
        public uint sectorLight {get;set;}

        public Pointer32_CollKDTree collKDTree;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CollKDTree _collKDTree {get {return collKDTree.collKDTree;} set {collKDTree.collKDTree = value;}}

        public ushort lodCount {get;set;}
        public ushort batchCount {get;set;}
        public Pointer32_IndexLODs lods;
        public Pointer32_BatchInfos batches;
        public List<IndexLOD> _lods {get {return lods.indexLODs;} set{lods.indexLODs = value;}}
        public List<BatchInfo> _batches {get {return batches.batchInfos;} set{batches.batchInfos = value;}}

        public GeometryAsset(HoArchive.MemoryStreamEndian file){
            boundSphere = new HoArchive.float4(file);
            boundAABB = new BoundAABB(file);
            materialID = file.ReadUInt64E();
            viewAttrib = new ViewAttrib(file);
            flags = file.ReadByte();
            wiiIndexStride = file.ReadByte();
            wiiIndexSkin = file.ReadByte();
            vertexCount = file.ReadUInt16E();
            prim = file.ReadByte();
            geomParamCount = file.ReadByte();
            rendParamCount = file.ReadByte();
            streamCount = file.ReadByte();
            streamMapBufferCount = file.ReadUInt16E();
            geomParams = new Pointer32_MaterialParams(file, geomParamCount);
            rendParams = new Pointer32_MaterialParams(file, rendParamCount);
            streams = new Pointer32_Streams(file, streamCount);
            streamMapBuffer = new Pointer32_b(file, streamMapBufferCount);
            sectorLight = file.ReadUInt32E();
            collKDTree = new Pointer32_CollKDTree(file);
            lodCount = file.ReadUInt16E();
            batchCount = file.ReadUInt16E();
            lods = new Pointer32_IndexLODs(file, lodCount);
            batches = new Pointer32_BatchInfos(file, batchCount);

        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);

            geomParams.Update();
            rendParams.Update();
            collKDTree.Update();

            geomParamCount = (byte)geomParams.materialParams.Count; 
            rendParamCount = (byte)rendParams.materialParams.Count;      
            streamCount = (byte)streams.streams.Count;
            streamMapBufferCount = (ushort)streamMapBuffer.b.Count;
            lodCount = (ushort)lods.indexLODs.Count;
            batchCount = (ushort)batches.batchInfos.Count;

        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            boundSphere.Save(file);
            boundAABB.Save(file);
            file.WriteE(materialID);
            viewAttrib.Save(file);
            file.WriteE(flags);
            file.WriteE(wiiIndexStride);
            file.WriteE(wiiIndexSkin);
            file.WriteE(vertexCount);
            file.WriteE(prim);
            file.WriteE(geomParamCount);
            file.WriteE(rendParamCount);
            file.WriteE(streamCount);
            file.WriteE(streamMapBufferCount);
            geomParams.SavePointer(file);
            rendParams.SavePointer(file);
            streams.SavePointer(file);
            streamMapBuffer.SavePointer(file);
            file.WriteE(sectorLight);
            collKDTree.SavePointer(file);
            file.WriteE(lodCount);
            file.WriteE(batchCount);
            lods.SavePointer(file);
            batches.SavePointer(file);
        }

        public void SaveHeap(MemoryStreamEndian file){
            collKDTree.Save(file);
            file.Align(0x10);
            streams.Save(file);
            lods.Save(file);
            batches.Save(file); // Might be different, seems to be unused anyways
            geomParams.Save(file);
            rendParams.Save(file);
            streamMapBuffer.Save(file);
            file.Align(0x10);

            foreach(MaterialParam param in geomParams.materialParams){
                param.SaveHeap(file);
            }
            foreach(MaterialParam param in rendParams.materialParams){
                param.SaveHeap(file);
            }
        }

        public List<ulong> GetBufferIDs(){
            List<ulong> output = new();
            foreach(VertexStream stream in _streams){
                output.Add(stream.vertexBufferID);
            }
            return output;
        }

        public List<ulong> GetRendTextureIDs(){
            List<ulong> output = new();
            foreach(MaterialParam param in _rendParams){
                if(param.type == 3){
                    foreach(SamplerParamData p in ((Pointer32_SamplerParamData)param.__anon).samp){
                        output.Add(p.textureID);
                    }
                }
            }
            return output;
        }

        public ulong GetMaterialID()
        {
            return materialID;
        }

        public ulong GetIndexBufferID()
        {
            return _lods[0].indexBufferID;
        }

        public Vector3 GetBoundSphereCenter()
        {
            return new Vector3(boundSphere.x, boundSphere.y, boundSphere.z);
        }

        public float GetBoundSphereRadius()
        {
            return boundSphere.w;
        }

        public Vector3 GetAABBCenter()
        {
            return (boundAABB.lower.GetVector3() + boundAABB.upper.GetVector3()) / 2;
        }
    }
}