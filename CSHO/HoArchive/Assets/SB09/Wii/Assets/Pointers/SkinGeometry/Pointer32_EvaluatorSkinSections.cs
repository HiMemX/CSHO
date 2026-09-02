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
            file.Jump(_p);
            evaluatorSkinSections = new List<EvaluatorSkinSection>();
            for(int x=0; x<count; x++){
                evaluatorSkinSections.Add(new EvaluatorSkinSection(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(EvaluatorSkinSection section in evaluatorSkinSections){
                section.Save(file);
            }
        }
    }
}