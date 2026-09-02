using System;
using System.Collections.Generic;
using System.Linq;

namespace SB09WiiAsset{
    public class Pointer32_LinkAssetsBaseNew : Pointer32
    {
        public List<LinkAssetBaseNew> events { get; set; }

        public Pointer32_LinkAssetsBaseNew(HoArchive.MemoryStreamEndian file, uint count) : base(file)
        {
            // Read events backwards

            events = new List<LinkAssetBaseNew>();

            if (count == 0) return;

            file.Jump(_p + (count - 1) * 0x28); // Skip to last linkassetbasenew

            int nextptr = (int)file.Length;
            for (int i = 0; i < count; i++)
            {
                events.Add(new LinkAssetBaseNew(file, nextptr));

                nextptr = (int)events.Last().srcEvent.v;

                if (i != count - 1) file.Position -= 2 * 0x28; // To prevent underflow issues
                /*try { }
                catch
                {
                    int oldptr = (int)file.Position;
                    file.Position = 0;
                    throw new System.Exception(file.ReadUInt64E().ToString("X16") + ", " + oldptr.ToString("X8") + ", " + nextptr.ToString("X8"));
                }*/
            }


            events.Reverse();

            file.Return();
        }

        public void Update(HoArchive.TOCEntry entry)
        {
        }

        public new void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);

            foreach (LinkAssetBaseNew link in events) link.Save(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file)
        {
            foreach (LinkAssetBaseNew link in events) link.SaveHeap(file);
        }
    }
}