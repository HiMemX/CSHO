using System.ComponentModel;

namespace SB09WiiAsset{
    public class PhysicsDataStruct{
        public float mass {get; set;} = new();
        public float friction {get; set;} = new();
        public float elasticity {get; set;} = new();
        public float linearDamping {get; set;} = new();
        public float angularDamping {get; set;} = new();
        public float settleTimer {get; set;} = new();
        public byte magnetic_Charge {get; set;} = new();
        public byte pad1 {get; set;} = new();
        public byte pad2 {get; set;} = new();
        public byte pad3 {get; set;} = new();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SoundSourcesPhysics SoundsPhysics {get; set;}

        public PhysicsDataStruct(){
            SoundsPhysics = new();
        }

        public PhysicsDataStruct(HoArchive.MemoryStreamEndian file){
            mass = file.ReadFloat32E();
            friction = file.ReadFloat32E();
            elasticity = file.ReadFloat32E();
            linearDamping = file.ReadFloat32E();
            angularDamping = file.ReadFloat32E();
            settleTimer = file.ReadFloat32E();
            magnetic_Charge = file.ReadByte();
            pad1 = file.ReadByte();
            pad2 = file.ReadByte();
            pad3 = file.ReadByte();
            SoundsPhysics = new SoundSourcesPhysics(file);
        }

        public void Update(HoArchive.TOCEntry entry){
            SoundsPhysics.Update();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(mass);
            file.WriteE(friction);
            file.WriteE(elasticity);
            file.WriteE(linearDamping);
            file.WriteE(angularDamping);
            file.WriteE(settleTimer);
            file.WriteE(magnetic_Charge);
            file.WriteE(pad1);
            file.WriteE(pad2);
            file.WriteE(pad3);
            SoundsPhysics.Save(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            SoundsPhysics.SaveHeap(file);
        }
    }
}