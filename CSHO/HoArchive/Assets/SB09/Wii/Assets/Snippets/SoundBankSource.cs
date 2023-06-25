using System.ComponentModel;

namespace SB09WiiAsset{
    public class SoundBankSource{
        public byte streamed {get; set;}
        public byte pad1 {get; set;}
        public byte pad2 {get; set;}
        public byte pad3 {get; set;}
        [TypeConverter(typeof(HoArchive.SB09WiiPaddedStringPointerConverter))]
        public Pointer32_paddedString sourceString {get; set;}
        [TypeConverter(typeof(HoArchive.SB09WiiSoundIndicesPointerConverter))]
        public Pointer32_soundIndices indices {get; set;}

        public SoundBankSource(HoArchive.MemoryStreamEndian file){
            streamed = file.ReadByte();
            pad1 = file.ReadByte();
            pad2 = file.ReadByte();
            pad3 = file.ReadByte();
            sourceString = new Pointer32_paddedString(file);
            indices = new Pointer32_soundIndices(file);
        }

        public void Update(){
            sourceString.Update();
            indices.Update();
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteByte(streamed);
            file.WriteByte(pad1);
            file.WriteByte(pad2);
            file.WriteByte(pad3);
            sourceString.SavePointer(file);
            indices.SavePointer(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            sourceString.Save(file);
            indices.Save(file);
        }
    }
}