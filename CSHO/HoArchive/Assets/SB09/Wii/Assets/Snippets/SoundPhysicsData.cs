namespace SB09WiiAsset{
    public class SoundPhysicsData{
        public float MinVelocity {get; set;} = new();
        public float MaxVelocity {get; set;} = new();
        public float MinRun {get; set;} = new();
        public float MaxRun {get; set;} = new();
        public float MaxRise {get; set;} = new();

        public SoundPhysicsData(){}

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