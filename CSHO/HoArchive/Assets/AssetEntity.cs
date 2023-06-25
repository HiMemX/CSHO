namespace Asset{
    public abstract class AssetEntity{
        public abstract void Save(HoArchive.MemoryStreamEndian file);
        public abstract void Update(HoArchive.TOCEntry entry);
    }
}