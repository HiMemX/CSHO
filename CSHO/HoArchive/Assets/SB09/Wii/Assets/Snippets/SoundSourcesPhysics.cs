using System.ComponentModel;

namespace SB09WiiAsset{
    public class SoundSourcesPhysics{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SoundBankSource SoundHit {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SoundBankSource SoundScrape {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SoundBankSource SoundRoll {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SoundPhysicsData PhysicsData {get; set;}

        public SoundSourcesPhysics(HoArchive.MemoryStreamEndian file){
            SoundHit = new SoundBankSource(file);
            SoundScrape = new SoundBankSource(file);
            SoundRoll = new SoundBankSource(file);
            PhysicsData = new SoundPhysicsData(file);
        }

        public void Update(){
            SoundHit.Update();
            SoundScrape.Update();
            SoundRoll.Update();
            PhysicsData.Update();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            SoundHit.Save(file);
            SoundScrape.Save(file);
            SoundRoll.Save(file);
            PhysicsData.Save(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            SoundHit.SaveHeap(file);
            SoundScrape.SaveHeap(file);
            SoundRoll.SaveHeap(file);
        }
    }
}