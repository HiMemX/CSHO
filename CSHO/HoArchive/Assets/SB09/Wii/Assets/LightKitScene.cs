using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class LightKitScene : Asset.AssetEntity{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong NPC {get; set;} = 0;
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong Player {get; set;} = 0;
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong Object {get; set;} = 0;
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong Environment {get; set;} = 0;

        public LightKitScene(){}

        public LightKitScene(HoArchive.MemoryStreamEndian file){
            NPC = file.ReadUInt64E();
            Player = file.ReadUInt64E();
            Object = file.ReadUInt64E();
            Environment = file.ReadUInt64E();
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(NPC);
            file.WriteE(Player);
            file.WriteE(Object);
            file.WriteE(Environment);
        }
    }
}