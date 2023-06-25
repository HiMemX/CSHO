using System.Collections.Generic;
using System.IO;

namespace HoArchive{
    public class StringTable{
        public List<string> StringTableEntries = new List<string>();
        public string DomainString;

        public StringTable(string DomainString_in){
            DomainString = DomainString_in;
        }

        public StringTable(BinaryReaderEndian file, long DomainStringPtr){
            while(file.BaseStream.Position < DomainStringPtr){
                file.BaseStream.Position += 0x0C;
                StringTableEntries.Add(file.ReadUntil(0));
                file.Align(0x04);
            }
            file.BaseStream.Position = DomainStringPtr;
            DomainString = file.ReadUntil(0);
        }

        public StringTable(BinaryReaderEndian file, uint stringTableLength){
            uint startpos = (uint)file.BaseStream.Position;
            while(file.BaseStream.Position < (stringTableLength + startpos)){
                file.BaseStream.Position += 0x0C;
                StringTableEntries.Add(file.ReadUntil(0));
                file.Align(0x04);
            }
            DomainString = "";
        }

        public void Update(){
        }

        public void Save(BinaryWriterEndian file, bool writeDomainString){
            foreach (string entry in StringTableEntries){  
                file.Pad(0x0C, 0xEE);
                file.WriteString(entry + "\0");
                file.PadAlign(0x04, 0x33);
            }
            if(writeDomainString){
                file.WriteString(DomainString + "\0");
            }
        }

        public void SaveLSET(StreamWriter file, string indent){
            file.WriteLine(indent + "StringTable(DomainString='" + DomainString + "'){");
            foreach(string entry in StringTableEntries){
                file.WriteLine(indent + "   " + entry);
            }

            file.WriteLine(indent + "}");
        }
    }
}