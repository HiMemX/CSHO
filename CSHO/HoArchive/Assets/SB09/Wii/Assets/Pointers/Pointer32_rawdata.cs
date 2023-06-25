using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_rawdata : Pointer32{
        public List<byte> data = new List<byte>();

        public Pointer32_rawdata(HoArchive.MemoryStreamEndian file, long datalen) : base(file){
            file.Jump(_p);
            if(datalen != 0){
                datalen = file.Length;
                while(file.Position < datalen){
                    data.Add(file.ReadByte());
                }
            }
            file.Return();
        }

        public void Update(HoArchive.TOCEntry entry){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.Write(data.ToArray());
        }
    }
}