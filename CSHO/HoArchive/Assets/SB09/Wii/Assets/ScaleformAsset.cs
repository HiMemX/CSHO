using System.Collections.Generic;
using System.Linq;
using HoArchive;

namespace SB09WiiAsset{
    public class ScaleformAsset : Asset.AssetEntity{
        public uint textureListOffset {get; set;}
        public uint numTextures {get; set;}
        public uint isFont {get; set;}
        public uint dataOffset {get; set;} // Data at multiples of 0x40
        
        public string name {get; set;} // Not in dwarf (Null terminated)
        public List<ScaleformTexture> textures {get; set;}
        //public List<ulong> textureIDs {get; set;} // Not in dwarf
        //public List<string> textureNames {get; set;} // Not in dwarf
        
        public List<byte> Scaleform; // Temporary until we make an actual editor (probably never, JPEXs is good enough)

        public ScaleformAsset(HoArchive.MemoryStreamEndian file){
            textureListOffset = file.ReadUInt32E();
            numTextures = file.ReadUInt32E();
            isFont = file.ReadUInt32E();
            dataOffset = file.ReadUInt32E();
            name = file.ReadUntil(0x00);

            file.Position = textureListOffset;
            textures = new();
            for(int i=0; i<numTextures; i++){
                textures.Add(new(file.ReadUInt64E(), ""));
            }
            for(int i=0; i<numTextures; i++){
                textures[i].name = file.ReadUntil(0x00);
            }

            file.Position = dataOffset;
            Scaleform = file.ReadBytes((int)(file.Length - file.Position)).ToList();            
        }

        public override void Update(HoArchive.TOCEntry entry){
            textureListOffset = 0x10 + MathTools.RoundUpTo((uint)name.Length + 1, 0x08);
            numTextures = (uint)textures.Count;
            
            dataOffset = textureListOffset + 0x8 * numTextures;
            foreach(ScaleformTexture texture in textures){
                dataOffset += (uint)texture.name.Length + 1;
            }
            dataOffset = MathTools.RoundUpTo(dataOffset, 0x40);
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(textureListOffset);
            file.WriteE(numTextures);
            file.WriteE(isFont);
            file.WriteE(dataOffset);

            file.WriteString(name + "\0");
            file.PadAlign(0x08, 0x00);

            foreach(ScaleformTexture texture in textures){
                file.WriteE(texture.ID);
            }
            foreach(ScaleformTexture texture in textures){
                file.WriteString(texture.name + "\0");
            }
            
            file.PadAlign(0x40, 0);
            file.Write(Scaleform.ToArray());

        }
    }
}