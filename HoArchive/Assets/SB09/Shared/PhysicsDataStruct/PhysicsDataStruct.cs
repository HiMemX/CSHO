
namespace SB09Assets{
    public class PhysicsDataStruct{
        public float mass;
        public float friction;
        public float elasticity;
        public float linearDamping;
        public float angularDamping;
        public float settleTimer;
        public byte magnetic_Charge;
        public byte pad1;
        public byte pad2;
        public byte pad3;
        public SoundSourcesPhysics SoundsPhysics;

        public PhysicsDataStruct(HoArchive.BinaryReaderEndian file){
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

        public Save())
    }
}