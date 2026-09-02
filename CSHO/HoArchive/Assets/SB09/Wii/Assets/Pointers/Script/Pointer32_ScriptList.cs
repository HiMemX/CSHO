using System.Collections.Generic;
using System.Linq;

namespace SB09WiiAsset{
    public class Pointer32_ScriptList : Pointer32
    {

        public List<ScriptEntry> scriptEntries { get; set; }

        public Pointer32_ScriptList(List<ScriptEntry> scriptEntries)
        {
            this.scriptEntries = scriptEntries;
        }

        public Pointer32_ScriptList(HoArchive.MemoryStreamEndian file, uint count, int argsectionend) : base(file)
        {
            /*file.Jump(_p);
            for(int i=0; i<count; i++){
                scriptEntries.Add(new ScriptEntry(file));
            }*/

            scriptEntries = new();
            file.Jump(_p + (count - 1) * 0x20); // Skip to last linkassetbasenew

            int nextptr = argsectionend;
            for (int i = 0; i < count; i++)
            {
                scriptEntries.Add(new ScriptEntry(file, nextptr));

                nextptr = (int)scriptEntries.Last().dstEvent.v;

                file.Position -= 2 * 0x20;
            }

            scriptEntries.Reverse();

            file.Return();
        }

        public new void Update()
        {
            foreach (ScriptEntry entry in scriptEntries) entry.Update();
        }

        public new void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);
            foreach (ScriptEntry ScriptEntry in scriptEntries)
            {
                ScriptEntry.Save(file);
            }
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file)
        {
            foreach (ScriptEntry scriptEntry in scriptEntries)
            {
                scriptEntry.SaveHeap(file);
            }
        }
    }
}