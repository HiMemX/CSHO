using System.ComponentModel;

namespace SB09WiiAsset{
    public class Timer : xBaseAsset{
        public float Value {get; set;} = new();
        public float Randomness {get; set;} = new();
        public bool runsInPause {get; set;} = new();
        public byte pad1 {get; set;} = new();
        public byte pad2 {get; set;} = new();
        public byte pad3 {get; set;} = new();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;} = new();

        public Timer() : base(){}


        public Timer(HoArchive.MemoryStreamEndian file) : base(file){
            Value = file.ReadFloat32E();
            Randomness = file.ReadFloat32E();
            runsInPause = file.ReadBool();
            pad1 = file.ReadByte();
            pad2 = file.ReadByte();
            pad3 = file.ReadByte();
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(Value);
            file.WriteE(Randomness);
            file.WriteE(runsInPause);
            file.WriteE(pad1);
            file.WriteE(pad2);
            file.WriteE(pad3);
            EventLinksNew.Save(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}