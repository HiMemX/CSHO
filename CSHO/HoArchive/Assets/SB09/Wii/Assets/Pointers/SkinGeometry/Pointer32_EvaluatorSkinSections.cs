using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_EvaluatorSkinSections : Pointer32{
        public List<EvaluatorSkinSection> evaluatorSkinSections {get; set;}

        public Pointer32_EvaluatorSkinSections(){
            evaluatorSkinSections = new List<EvaluatorSkinSection>();
        }

        public Pointer32_EvaluatorSkinSections(List<EvaluatorSkinSection> evaluatorSkinSections){
            this.evaluatorSkinSections = evaluatorSkinSections;
        }

        public Pointer32_EvaluatorSkinSections(HoArchive.MemoryStreamEndian file, ushort count) : base(file){
            evaluatorSkinSections = ReadItems<EvaluatorSkinSection>(file, count, stream => new EvaluatorSkinSection(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, evaluatorSkinSections, (stream, item) => item.Save(stream));
        }
    }
}