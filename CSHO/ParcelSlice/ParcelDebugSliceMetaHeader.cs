class ParcelDebugSliceMetaHeader{
    public uint metaType = 1347636292;
    public uint metaSize;
    public ParcelDebugSliceMetaHeader(BinaryReaderEndian file){
        metaSize  = file.ReadUInt32E();
    }
    
    public void Update(){} // Header doesn't need updating ()

    public void Save(BinaryWriterEndian file){
        file.WriteString("PSLD");
        file.WriteE(metaSize);
    }
}