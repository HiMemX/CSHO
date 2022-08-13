using System.Collections.Generic;

namespace HoArchive{
    public class Archive{
        public Header Header;
        public Table MasterTable;
        public Archive(BinaryReaderEndian file){
            Header = new Header(file);

            file.BaseStream.Position = Header.startSector * Header.sectorSize;
            MasterTable = new Table(file, Header.sectorSize);
        }

        public void Update(){
            uint startSector  = MathTools.CeilDiv(0x800, Header.sectorSize);
            Header.startSector = startSector;

            MasterTable.Update(0x40, Header.sectorSize, false); // 0x40 is default alignment
            MasterTable.updateSectors(Header.sectorSize, startSector);
            Header.Update(MasterTable);
        }

        public void Save(BinaryWriterEndian file){
            Header.Save(file);
            
            file.PadTo(Header.sectorSize*Header.startSector, 0x00);

            MasterTable.Save(file, Header.sectorSize, new List<SliceMeta>());
        }
    }
}