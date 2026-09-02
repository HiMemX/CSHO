using System;
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset
{
    public class NPCGeneric : xBaseAsset
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public xEntAsset EntAsset { get; set; }

        // 0x110
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong LightKitID { get; set; }
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong ChrAssetID { get; set; }
        [TypeConverter(typeof(RGBA8888Converter))]
        public uint shadowColor { get; set; }
        public float shadowMaxDepth { get; set; }
        public float shadowStartDepth { get; set; }
        public uint shadowMinBlur { get; set; }
        public uint shadowMaxBlur { get; set; }

        // 0x138
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong NPCTemplate { get; set; }
        public uint EnemyFlags { get; set; }

        // 0x148
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong WallNet { get; set; }
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong MovePoint { get; set; }
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong MovePointNetwork { get; set; }
        public uint SpawnType { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew { get; set; }

        //The following region (aswell as the 0x10 bytes before) are unknown.
        public uint unknown0 { get; set; }
        public uint unknown1 { get; set; }
        public uint unknown2 { get; set; }
        public uint unknown3 { get; set; }
        public float unknown4 { get; set; }
        public float unknown5 { get; set; }
        public float unknown6 { get; set; }
        public uint unknown7 { get; set; }
        public uint unknown8 { get; set; }
        public uint unknown9 { get; set; }
        public uint unknown10 { get; set; }
        public uint unknown11 { get; set; }
        public float unknown12 { get; set; }
        public float unknown13 { get; set; }
        public uint unknown14 { get; set; }
        public uint unknown15 { get; set; }
        public uint unknown16 { get; set; }

        public NPCGeneric(HoArchive.MemoryStreamEndian file) : base(file)
        {
            //Console.WriteLine("Test0");
            EntAsset = new xEntAsset(file);
            //Console.WriteLine("Test1");
            file.Align(0x10);
            LightKitID = file.ReadUInt64E();
            ChrAssetID = file.ReadUInt64E();
            shadowColor = file.ReadUInt32E();
            shadowMaxDepth = file.ReadFloat32E();
            shadowStartDepth = file.ReadFloat32E();
            shadowMinBlur = file.ReadUInt32E();
            shadowMaxBlur = file.ReadUInt32E();

            file.Align(0x08);
            NPCTemplate = file.ReadUInt64E();
            EnemyFlags = file.ReadUInt32E();

            file.Align(0x08);
            WallNet = file.ReadUInt64E();
            MovePoint = file.ReadUInt64E();
            MovePointNetwork = file.ReadUInt64E();
            SpawnType = file.ReadUInt32E();

            EventLinksNew = new LinkAsset(file);
            //Console.WriteLine("Test2");

            unknown0 = file.ReadUInt32E();
            unknown1 = file.ReadUInt32E();
            unknown2 = file.ReadUInt32E();
            unknown3 = file.ReadUInt32E();
            unknown4 = file.ReadFloat32E();
            unknown5 = file.ReadFloat32E();
            unknown6 = file.ReadFloat32E();
            unknown7 = file.ReadUInt32E();
            unknown8 = file.ReadUInt32E();
            unknown9 = file.ReadUInt32E();
            unknown10 = file.ReadUInt32E();
            unknown11 = file.ReadUInt32E();
            unknown12 = file.ReadFloat32E();
            unknown13 = file.ReadFloat32E();
            unknown14 = file.ReadUInt32E();
            unknown15 = file.ReadUInt32E();
            unknown16 = file.ReadUInt32E();
        }

        public override void Update(HoArchive.TOCEntry entry)
        {
            base.Update(entry);
            EntAsset.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);
            EntAsset.Save(file);
            file.PadAlign(0x10, 0);

            file.WriteE(LightKitID);
            file.WriteE(ChrAssetID);
            file.WriteE(shadowColor);
            file.WriteE(shadowMaxDepth);
            file.WriteE(shadowStartDepth);
            file.WriteE(shadowMinBlur);
            file.WriteE(shadowMaxBlur);

            file.Align(0x08);
            file.WriteE(NPCTemplate);
            file.WriteE(EnemyFlags);

            file.Align(0x08);
            file.WriteE(WallNet);
            file.WriteE(MovePoint);
            file.WriteE(MovePointNetwork);
            file.WriteE(SpawnType);

            EventLinksNew.Save(file);

            file.WriteE(unknown0);
            file.WriteE(unknown1);
            file.WriteE(unknown2);
            file.WriteE(unknown3);
            file.WriteE(unknown4);
            file.WriteE(unknown5);
            file.WriteE(unknown6);
            file.WriteE(unknown7);
            file.WriteE(unknown8);
            file.WriteE(unknown9);
            file.WriteE(unknown10);
            file.WriteE(unknown11);
            file.WriteE(unknown12);
            file.WriteE(unknown13);
            file.WriteE(unknown14);
            file.WriteE(unknown15);
            file.WriteE(unknown16);

            EntAsset.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}