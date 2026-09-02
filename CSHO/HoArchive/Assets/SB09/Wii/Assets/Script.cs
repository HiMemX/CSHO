using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SB09WiiAsset{
    public class Script : xBaseAsset{
        public Pointer32_ScriptList ScriptList;
        public List<ScriptEntry> _ScriptList {get {return ScriptList.scriptEntries;} set {ScriptList.scriptEntries = value;}}

        public float ScaleFactor {get; set;}
        public uint eventCount {get; set;}
        public bool Loop {get; set;}
        public bool RunWhenPaused {get; set;}
        public bool RunInCinematic {get; set;}
        public byte pad2 {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get;set;}


        public Script(HoArchive.MemoryStreamEndian file) : base(file){
            uint count = file.ReadUInt32E();

            file.Jump(0x24);
            EventLinksNew = new(file);
            file.Return();

            ScriptList = new(file, count, (int)EventLinksNew.EventLinksArray.linkAssetsBaseNew._p);
            ScaleFactor = file.ReadFloat32E();
            eventCount = file.ReadUInt32E();
            Loop = file.ReadBool();
            RunWhenPaused = file.ReadBool();
            RunInCinematic = file.ReadBool();
            pad2 = file.ReadByte();

        }

        public override void Update(HoArchive.TOCEntry entry)
        {
            base.Update(entry);
            eventCount = (uint)ScriptList.scriptEntries.Count;
            ScriptList.Update();
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            file.WriteE((uint)ScriptList.scriptEntries.Count);
            ScriptList.SavePointer(file);
            file.WriteE(ScaleFactor);
            file.WriteE(eventCount);
            file.WriteE(Loop);
            file.WriteE(RunWhenPaused);
            file.WriteE(RunInCinematic);
            file.WriteE(pad2);

            EventLinksNew.Save(file);
            file.Align(0x10);
            ScriptList.Save(file);

            ScriptList.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}