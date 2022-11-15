using System.Collections.Generic;


namespace HoArchive{
    public class ParcelTOC{
        public TOCHeader Header;
        public List<TOCEntry> Entries = new List<TOCEntry>();

        public bool delete = false;

        public ParcelTOC(){
            Header = new TOCHeader();
        }
        public ParcelTOC(BinaryReaderEndian file, uint DataPtr, string target){
            Header = new TOCHeader(file);

            TOCEntry Entry;
            for (int element=0; element<Header.elementCount; element++){
                Entry = new TOCEntry(file, DataPtr, target);
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
    }
}