using System.ComponentModel;
using Asset;

namespace SB09WiiAsset
{
    public class __srcEvent__
    {
        [TypeConverter(typeof(EventConverter))]
        public Event type { get; set; }
        public uint v { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EventEntity eventEntity { get; set; }

        public __srcEvent__()
        {
            type = Event.On;
            v = 0;
            eventEntity = null;
        }

        public __srcEvent__(HoArchive.MemoryStreamEndian file)
        {
            type = (Event)file.ReadUInt32E();
            v = file.ReadUInt32E();
        }

        public void Update()
        {
        }

        public void Save(HoArchive.MemoryStreamEndian file)
        {
            file.WriteE((uint)type);
            file.WriteE(v);
        }
    }
}