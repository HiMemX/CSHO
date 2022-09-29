
namespace SB09Assets{
    public class SoundSourcesPhysics{
        public SoundBankSource SoundHit;
        public SoundBankSource SoundScrape;
        public SoundBankSource SoundRoll;
        public SoundPhysicsData PhysicsData;

        public SoundSourcesPhysics(HoArchive.BinaryReaderEndian file){
            SoundHit = new SoundBankSource(file);
            SoundScrape = new SoundBankSource(file);
            SoundRoll = new SoundBankSource(file);
            PhysicsData = new SoundPhysicsData(file);
        }
    }
}