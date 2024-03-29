using System.Collections.Generic;
using System.IO;

namespace HoArchive{
    public interface ParcelBase{
        public void Update(uint Align, uint sectorSize, bool Reversed);
        public uint getSize(List<SliceMeta> meta);
        public uint getTotalSize(List<SliceMeta> meta, uint sectorSize);
        public void updateSectors(uint sectorSize, uint startSector);
        public void Save(BinaryWriterEndian file, uint sectorSize, List<SliceMeta> metas);// uint sectorSize, uint memoryAlignment);
        public void SaveLSET(StreamWriter file, string indent, string tag, List<NameTableEntry> nameTableEntries, TableEntry entry = null);
    }
}