
using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class Effect : Asset.AssetEntity{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MaterialParamFormatTableAsset_Array3 paramFormats {get; set; } // Not a usual array because of fixed size, this makes editing them easier
        public byte techCount; // As in Technique
        public byte lodCount;
        public ushort featureCount;
        public ushort passCount;
        public ushort shaderCount;
        public ushort effectFlags {get; set;}
        // 6 bytes padding

        // Lists not in DWARF
        public List<Shader> shaders {get; set;}
        public List<Technique> techniques {get; set;}
        public List<Feature> features {get; set;}
        public List<LOD> lods {get; set;}
        public List<Pass> passes {get; set;}

        public Effect(HoArchive.MemoryStreamEndian file){
            paramFormats = new MaterialParamFormatTableAsset_Array3(new List<MaterialParamFormatTableAsset> {new(file), new(file), new(file)});
            techCount = file.ReadByte();
            lodCount = file.ReadByte();
            featureCount = file.ReadUInt16E();
            passCount = file.ReadUInt16E();
            shaderCount = file.ReadUInt16E();
            effectFlags = file.ReadUInt16E();

            file.Position += 0x06; // Padding bytes

            shaders = new();
            techniques = new();
            features = new();
            lods = new();
            passes = new();

            for(int i=0; i<shaderCount; i++){ shaders.Add(new Shader(file));}
            for(int i=0; i<techCount; i++){ techniques.Add(new Technique(file));}
            for(int i=0; i<featureCount; i++){ features.Add(new Feature(file));}
            for(int i=0; i<lodCount; i++){ lods.Add(new LOD(file));}
            for(int i=0; i<passCount; i++){ passes.Add(new Pass(file));}
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);

            paramFormats.element0.Update();
            paramFormats.element1.Update();
            paramFormats.element2.Update();
            
            shaderCount = (ushort)shaders.Count;
            techCount = (byte)techniques.Count;
            featureCount = (ushort)features.Count;
            lodCount = (byte)lods.Count;
            passCount = (ushort)passes.Count;

            for(int i=0; i<shaderCount; i++){
                shaders[i].Update();
            }
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            paramFormats.element0.Save(file);
            paramFormats.element1.Save(file);
            paramFormats.element2.Save(file);

            file.WriteE(techCount);
            file.WriteE(lodCount);
            file.WriteE(featureCount);
            file.WriteE(passCount);
            file.WriteE(shaderCount);
            file.WriteE(effectFlags);
            file.Pad(6, 0);

            for(int i=0; i<shaderCount; i++){ shaders[i].Save(file); }
            for(int i=0; i<techCount; i++){ techniques[i].Save(file); }
            for(int i=0; i<featureCount; i++){ features[i].Save(file); }
            for(int i=0; i<lodCount; i++){ lods[i].Save(file); }
            for(int i=0; i<passCount; i++){ passes[i].Save(file); }

            file.Align(0x04);

            paramFormats.element0.SaveHeap(file);
            paramFormats.element1.SaveHeap(file);
            paramFormats.element2.SaveHeap(file);

            for(int i=0; i<shaderCount; i++){ shaders[i].SaveHeap(file); }
            for(int i=0; i<techCount; i++){ techniques[i].SaveHeap(file); }
            
            for(int t=0; t<paramFormats.element0.entries.techniques.Count; t++){
                paramFormats.element0.entries.techniques[t].SaveHeap(file);
            }
            for(int t=0; t<paramFormats.element1.entries.techniques.Count; t++){
                paramFormats.element1.entries.techniques[t].SaveHeap(file);
            }
            for(int t=0; t<paramFormats.element2.entries.techniques.Count; t++){
                paramFormats.element2.entries.techniques[t].SaveHeap(file);
            }
        }

        public ulong GetShaderID()
        {
            
            foreach(Shader s in shaders){
                if(s.paramIndices._element0.b.Count > 0){ return s.shaderID; }
            }

            return 0;
        }

        public ulong GetRenderModeID(){
            foreach(Shader s in shaders){
                if(s.paramIndices._element0.b.Count > 0){ return s.renderModeID; }
            }

            return 0;
        }
    }
}