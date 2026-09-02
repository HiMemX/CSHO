using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HoArchive{
    public class ParcelTOC{
        public TOCHeader Header;
        public List<TOCEntry> Entries = new List<TOCEntry>();

        public bool delete = false;

        public List<Action<TOCEntry>> OnAddTOCEntry = new();

        public void AddTOCEntry(TOCEntry entry){
            Entries.Add(entry);

            foreach(Action<TOCEntry> action in OnAddTOCEntry){
                action(entry);
            }
        }

        public void AddTOCEntries(List<TOCEntry> entries)
        {
            Entries.AddRange(entries);

            foreach (TOCEntry entry in entries)
            {
                foreach (Action<TOCEntry> action in OnAddTOCEntry) action(entry);
            }
        }

        public ParcelTOC()
        {
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

        public bool Contains(ulong uid){
            foreach(TOCEntry entry in Entries){
                if(entry.uidSelf == uid) return true;
            }
            return false;
        }

        public TOCEntry GetEntry(ulong uid){
            foreach(TOCEntry entry in Entries){
                if(entry.uidSelf == uid) return entry;
            }
            return null;
        }

        public void Update(uint Align){
            for(int i=0; i<Entries.Count; i++){
                if(!Entries[i].delete){continue;}
                Entries.RemoveAt(i);
                i = 0;
            }

            Parallel.ForEach (Entries, element => {
                element.Update(Align);
            });
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