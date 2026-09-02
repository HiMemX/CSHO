


using System.ComponentModel;

namespace SB09WiiAsset{
    public class BatchInfo{
        public ushort vertStart {get;set;}
        public ushort vertCount {get;set;}
        public ushort triStart {get;set;}
        public ushort triCount {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SkinCount_Array4 skinCount {get;set;}

        public BatchInfo(){
            skinCount = new SkinCount_Array4();
        }

        public BatchInfo(HoArchive.MemoryStreamEndian file){
            vertStart = file.ReadUInt16E();
            vertCount = file.ReadUInt16E();
            triStart = file.ReadUInt16E();
            triCount = file.ReadUInt16E();
            skinCount = new SkinCount_Array4(file);
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(vertStart);
            file.WriteE(vertCount);
            file.WriteE(triStart);
            file.WriteE(triCount);
            skinCount.Save(file);
        }
    }
}