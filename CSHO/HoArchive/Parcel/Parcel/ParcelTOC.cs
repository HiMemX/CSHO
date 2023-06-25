using System.Collections.Generic;
using System.IO;

namespace HoArchive{
    public class ParcelTOC{
        public TOCHeader Header;
        public List<TOCEntry> Entries = new List<TOCEntry>();

        public bool delete = false;

        public ParcelTOC(){
            Header = new TOCHeader();
        }
        public ParcelTOC(BinaryReaderEndian file, uint DataPtr, string target, string platform){
            Header = new TOCHeader(file);

            TOCEntry Entry;
            for (int element=0; element<Header.elementCount; element++){
                Entry = new TOCEntry(file, DataPtr, target, platform);
                DataPtr += Entry.elementSize;

                Entries.Add(Entry);
            }
        }
        public void Update(uint Align){
            for(int i=0; i<Entries.Count; i++){
                if(!Entries[i].delete){continue;}
                Entries.RemoveAt(i);
                i = 0;
            }

            foreach (TOCEntry element in Entries){
                element.Update(Align);
            }
            Header.elementCount = (uint)Entries.Count; // Header doesn't have update because it's unnecessary
        }

        public void SaveData(BinaryWriterEndian file){
            foreach (TOCEntry entry in Entries){
                entry.SaveData(file);
            }
        }
        public void SaveMeta(BinaryWriterEndian file){
            foreach (TOCEntry entry in Entries){
                entry.SaveMeta(file);
            }
        }

        public void SaveLSET(StreamWriter file, string indent, List<NameTableEntry> nameTableEntries){
            file.WriteLine(indent + "TOC{");

            foreach(TOCEntry entry in Entries){
                entry.SaveLSET(file, indent + "   ", nameTableEntries);
            }

            file.WriteLine(indent + "}");
        }

        public ParcelTOC(List<string> lines, ParcelDebug debugParcel, string game, string platform, string assetpath){
            Header = new TOCHeader();
            foreach(string line in lines){
                if(line.Length == 0){continue;}
                if(line[0] != '$'){continue;}

                Entries.Add(new TOCEntry(line, debugParcel, game, platform, assetpath));
            }
        }
    }
}