using System.Collections.Generic;

namespace SB09WiiAsset{
    public class __EventLinksArray__{
        public uint count;
        //public Pointer32_rawdata data; // Used for actual writing for now
        public Pointer32_LinkAssetsBaseNew linkAssetsBaseNew {get; set;}
        public List<LinkAssetBaseNew> _linkAssetsBaseNew {get {return linkAssetsBaseNew.events;} set {linkAssetsBaseNew.events = value;}}

        public __EventLinksArray__(HoArchive.MemoryStreamEndian file){
            count = file.ReadUInt32E();

            // Problem: We don't have all event params documented, we need to load unknown event args in as byte list
            // -> We don't know the length of bytes
            // Solution: We load link entrys backwards, so that we can see where the next event args start

            /* Old raw data approach
            file.Jump((uint)file.Position);
            data = new Pointer32_rawdata(file, count);
            file.Return();*/
            linkAssetsBaseNew = new Pointer32_LinkAssetsBaseNew(file, count);
        }

        public void Update()
        {
            count = (uint)_linkAssetsBaseNew.Count;
            //if (count != linkAssetsBaseNew.events.Count) throw new System.Exception("HUh");
            for (int i = 0; i < count; i++) linkAssetsBaseNew.events[i].Update(); 
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(count);
            linkAssetsBaseNew.SavePointer(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file)
        {
            linkAssetsBaseNew.Save(file);
            linkAssetsBaseNew.SaveHeap(file); // Hardcoded for now, might need to adjust if weird asset type pops up
        }
    }
}