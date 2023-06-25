namespace SB09WiiAsset{
    public class Counter : xBaseAsset{
        public short Value {get; set;}
        public LinkAsset EventLinksNew;

        public Counter(HoArchive.MemoryStreamEndian file) : base(file){
            Value = file.ReadInt16E();
            file.Position += 0x02;
            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE(Value);
            file.Pad(2, 0x00);
            EventLinksNew.Save(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}