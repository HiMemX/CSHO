namespace SB09WiiAsset{
    public class instanceParamHandle{
        public uint part1 {get; set;}
        public uint part2 {get; set;}

        public instanceParamHandle(HoArchive.MemoryStreamEndian file){
            part1 = file.ReadUInt32E();
            part2 = file.ReadUInt32E();
        }

        public void Update(){}

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(part1);
            file.WriteE(part2);
        }
    }
}