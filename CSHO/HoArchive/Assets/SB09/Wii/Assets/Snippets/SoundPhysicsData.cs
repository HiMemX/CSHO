namespace SB09WiiAsset{
    public class SoundPhysicsData{
        public float MinVelocity {get; set;}
        public float MaxVelocity {get; set;}
        public float MinRun {get; set;}
        public float MaxRun {get; set;}
        public float MaxRise {get; set;}

        public SoundPhysicsData(HoArchive.MemoryStreamEndian file){
            MinVelocity = file.ReadFloat32E();
            MaxVelocity = file.ReadFloat32E();
            MinRun = file.ReadFloat32E();
            MaxRun = file.ReadFloat32E();
            MaxRise = file.ReadFloat32E();
        }

        public void Update(){
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(MinVelocity);
            file.WriteE(MaxVelocity);
            file.WriteE(MinRun);
            file.WriteE(MaxRun);
            file.WriteE(MaxRise);
        }
    }
}