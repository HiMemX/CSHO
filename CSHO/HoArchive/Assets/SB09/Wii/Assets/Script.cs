using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SB09WiiAsset{
    public class Script : xBaseAsset{
        public Pointer32_ScriptList ScriptList = new();
        public List<ScriptEntry> _ScriptList {get {return ScriptList.scriptEntries;} set {ScriptList.scriptEntries = value;}}

        public float ScaleFactor {get; set;} = new();
        public uint eventCount {get; set;} = new();
        public bool Loop {get; set;} = new();
        public bool RunWhenPaused {get; set;} = new();
        public bool RunInCinematic {get; set;} = new();
        public byte pad2 {get;set;} = new();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get;set;} = new();

        public Script() : base(){}

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