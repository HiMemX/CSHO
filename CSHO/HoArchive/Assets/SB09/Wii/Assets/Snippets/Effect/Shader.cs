using System;
using System.ComponentModel;
using HoArchive;
using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Shader{
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong shaderID {get; set;}
        [TypeConverter(typeof(AssetIDConverter))]
        public ulong renderModeID {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32_b_Array3 paramIndices {get;set;}
        public byte[] paramCount;
        public byte pad {get; set;}

        public Shader(){
            paramIndices = new Pointer32_b_Array3(new List<Pointer32_b>{new Pointer32_b(), new Pointer32_b(), new Pointer32_b()});
            paramCount = new byte[3] {0, 0, 0};
        }

        public Shader(HoArchive.MemoryStreamEndian file){
            shaderID = file.ReadUInt64E();
            renderModeID = file.ReadUInt64E();

            // Counts are after the pointer so we have to do a bit of grabbing ahead
            file.Position = file.Position + 0x0C;
            paramCount = new byte[3] {file.ReadByte(), file.ReadByte(), file.ReadByte()};
            file.Position = file.Position - 0x0F;
            paramIndices = new Pointer32_b_Array3(new List<Pointer32_b> {new Pointer32_b(file, paramCount[0]), new Pointer32_b(file, paramCount[1]), new Pointer32_b(file, paramCount[2])});
            file.Position = file.Position + 0x03;
            
            pad = file.ReadByte();
        }

        public void Update(){
            paramCount = new byte[3] {(byte)paramIndices._element0.b.Count, (byte)paramIndices._element1.b.Count, (byte)paramIndices._element2.b.Count};
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(shaderID);
            file.WriteE(renderModeID);
            
            paramIndices._element0.SavePointer(file);
            paramIndices._element1.SavePointer(file);
            paramIndices._element2.SavePointer(file);

            for(int i=0; i<3; i++){
                file.WriteE(paramCount[i]);
            }

            file.WriteE(pad);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            paramIndices._element0.Save(file);
            paramIndices._element1.Save(file);
            paramIndices._element2.Save(file);
        }
    }
}