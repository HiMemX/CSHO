using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SB09WiiAsset{
    public class Conditional : xBaseAsset{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ConditionDef Condition { get; set; }
        public uint LogicalOperator { get; set; }

        public Pointer32_MoreConditions MoreConditions;
        public List<ConditionDef> _MoreConditions { get { return MoreConditions.MoreConditions; } set { MoreConditions.MoreConditions = value; }}

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew { get; set; }



        public Conditional(HoArchive.MemoryStreamEndian file) : base(file)
        {
            Condition = new ConditionDef(file);
            file.Align(0x08);
            LogicalOperator = file.ReadUInt32E();
            MoreConditions = new Pointer32_MoreConditions(file, file.ReadUInt32E());

            //if (MoreConditions.count != 0) throw new NotImplementedException(id.ToString("X16"));

            EventLinksNew = new LinkAsset(file);
        }

        public override void Update(HoArchive.TOCEntry entry)
        {
            base.Update(entry);
            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file)
        {
            base.Save(file);
            Condition.Save(file);
            file.PadAlign(0x08, 0);
            file.WriteE(LogicalOperator);
            MoreConditions.SavePointer(file);
            EventLinksNew.Save(file);

            file.Align(0x08);

            MoreConditions.Save(file);

            file.Align(0x08);

            EventLinksNew.SaveHeap(file);
        }
    }
}