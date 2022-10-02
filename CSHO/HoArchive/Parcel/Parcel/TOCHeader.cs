
namespace HoArchive{
    public class TOCHeader{
        public uint elementCount;
        public uint brickDataOffset;
        public uint[] reserved = new uint[6];
        public TOCHeader(BinaryReaderEndian file){
            elementCount    = file.ReadUInt32E();
            brickDataOffset = file.ReadUInt32E();

            for (int i=0; i<6; i++){
                reserved[i] = file.ReadUInt32E();
            }
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(elementCount);
            file.WriteE(brickDataOffset);
            foreach (uint reserve in reserved){
                file.WriteE(reserve);
            }
        }
    }
}