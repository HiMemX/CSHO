using System.ComponentModel;
using HoArchive;
using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Technique{
        [TypeConverter(typeof(SB09WiiStringPointerConverter))]
        public Pointer32_string name {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public RenderStageEntries stages {get; set;} // always 4 entries
        public byte lodMax {get; set;}
        public byte renderStateFlags {get; set;}
        public ushort pad {get; set;} // In dwarf this is an array of 2 bytes, cbf doing that though

        public Technique(){
            name = new Pointer32_string("");
            stages = new RenderStageEntries(new List<RenderStageEntry>() {new RenderStageEntry(), new RenderStageEntry(), new RenderStageEntry(), new RenderStageEntry()});
        }

        public Technique(HoArchive.MemoryStreamEndian file){
            name = new Pointer32_string(file);
            stages = new RenderStageEntries(new List<RenderStageEntry>() {new RenderStageEntry(file),new RenderStageEntry(file),new RenderStageEntry(file),new RenderStageEntry(file)});
            lodMax = file.ReadByte();
            renderStateFlags = file.ReadByte();
            pad = file.ReadUInt16E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            name.SavePointer(file);
            
            stages.element0.Save(file);
            stages.element1.Save(file);
            stages.element2.Save(file);
            stages.element3.Save(file);

            file.WriteE(lodMax);
            file.WriteE(renderStateFlags);
            file.WriteE(pad);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            name.Save(file);
        }
    }
}