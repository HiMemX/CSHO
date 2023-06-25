using System.ComponentModel;
using System.Collections.Generic;


namespace SB09WiiAsset{
    public class Material : Asset.AssetEntity{
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong effectID {get; set;}
        [TypeConverter(typeof(HoArchive.AssetIDConverter))]
        public ulong renderModeID {get; set;}
        public float depthBias {get; set;}
        public byte materialParamCount {get; set;}
        public byte pad {get; set;}
        public ushort materialFlags {get; set;}
        
        public List<MaterialParam> materialParams {get; set;}


        

        public Material(HoArchive.MemoryStreamEndian file){
            effectID = file.ReadUInt64E();
            renderModeID = file.ReadUInt64E();
            depthBias = file.ReadFloat32E();
            materialParamCount = file.ReadByte();
            pad = file.ReadByte();
            materialFlags = file.ReadUInt16E();
            materialParams = new List<MaterialParam>();
            for(int i=0; i<materialParamCount; i++){
                materialParams.Add(new MaterialParam(file));
            }
        }

        public override void Update(HoArchive.TOCEntry entry){
            materialParamCount = (byte)materialParams.Count;
            foreach(MaterialParam param in materialParams){
                param.Update();
            }
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(effectID);
            file.WriteE(renderModeID);
            file.WriteE(depthBias);
            file.WriteE(materialParamCount);
            file.WriteE(pad);
            file.WriteE(materialFlags);
            foreach(MaterialParam param in materialParams){
                param.Save(file);
            }
            file.PadAlign(0x10, 0x00);
            foreach(MaterialParam param in materialParams){
                param.SaveHeap(file);
            }
        }
    }
}