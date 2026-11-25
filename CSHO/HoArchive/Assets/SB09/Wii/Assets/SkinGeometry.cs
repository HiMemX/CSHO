using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SB09WiiAsset{
    public class SkinGeometry : GeometryAsset{
        public byte skinFlags {get;set;} = new();
        public byte jointCount {get;set;} = new();
        public byte influenceSectionCount {get;set;} = new();
        public byte staticStreamCount { get; set; } = new();

        public Pointer32_EvaluatorSkinSections influenceSections = new();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public List<EvaluatorSkinSection> _influenceSections { get { return influenceSections.evaluatorSkinSections; } set { influenceSections.evaluatorSkinSections = value; } }
        public Pointer32_b staticStreamMap { get; set; } = new();
        public Pointer32_b skinBufferMap { get; set; } = new();
        public Pointer32_b animBoundBoneIndexData { get; set; } = new();
        public Pointer32_float4s animBoundSphereData { get; set; } = new();
        public byte[] dynStreamMap {get; set;} = new byte[4];// 4 bytes
        public byte dynConfig {get;set;} = new();
        public byte animBoundDataCount {get;set;} = new();
        public ushort wiiPaletteCount {get;set;} = new();
        //public Pointer32_WiiPaletteList wiiPaletteList;
        public uint wiiPaletteList {get;set;} = new();
        public uint morphCount {get;set;} = new();
        public uint morphList {get;set;} = new();

        public List<byte> restData {get;set;} = new();

        public SkinGeometry() : base(){}

        public SkinGeometry(HoArchive.MemoryStreamEndian file) : base(file){
            
            skinFlags = file.ReadByte();
            jointCount = file.ReadByte();
            influenceSectionCount = file.ReadByte();
            staticStreamCount = file.ReadByte();

            file.Jump(0x7d);
            animBoundDataCount = file.ReadByte(); // Need this data beforehand
            file.Return();

            influenceSections = new Pointer32_EvaluatorSkinSections(file, influenceSectionCount);
            staticStreamMap = new Pointer32_b(file, staticStreamCount);
            skinBufferMap = new Pointer32_b(file, (ushort)(4 +staticStreamCount)); // Seems to be consistent, no proof of this though yet
            animBoundBoneIndexData = new Pointer32_b(file, animBoundDataCount);
            animBoundSphereData = new Pointer32_float4s(file, animBoundDataCount);

            dynStreamMap = new byte[4] {file.ReadByte(),file.ReadByte(),file.ReadByte(),file.ReadByte()};
            dynConfig = file.ReadByte();
            file.ReadByte();
            wiiPaletteCount = file.ReadUInt16E();
            wiiPaletteList = file.ReadUInt32E();
            morphCount = file.ReadUInt32E();
            morphList = file.ReadUInt32E();

            file.Position = animBoundSphereData._p + 0x10 * animBoundDataCount;
            restData = file.ReadBytes((int)(file.Length - file.Position)).ToList();
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);

            file.WriteE(skinFlags);
            file.WriteE(jointCount);
            file.WriteE(influenceSectionCount);
            file.WriteE(staticStreamCount);

            influenceSections.SavePointer(file);
            staticStreamMap.SavePointer(file);
            skinBufferMap.SavePointer(file);
            animBoundBoneIndexData.SavePointer(file);
            animBoundSphereData.SavePointer(file);

            for(int i=0; i<4; i++){
                file.WriteE(dynStreamMap[i]);
            }
            file.WriteE(dynConfig);
            file.WriteE(animBoundDataCount);
            file.WriteE(wiiPaletteCount);
            file.WriteE(wiiPaletteList);
            file.WriteE(morphCount);
            file.WriteE(morphList);

            collKDTree._p = 0;
            batches._p = 0;
            base.SaveHeap(file);

            influenceSections.Save(file);
            staticStreamMap.Save(file);
            skinBufferMap.Save(file);
            animBoundBoneIndexData.Save(file);
            file.Align(0x10);
            animBoundSphereData.Save(file);

            file.Write(restData.ToArray());
        }
    }
}