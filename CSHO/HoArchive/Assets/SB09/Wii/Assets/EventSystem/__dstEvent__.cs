using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asset;
using SB09WiiEvent;

namespace SB09WiiAsset
{
    public class __dstEvent__
    {
        Event oldtype;
        [TypeConverter(typeof(EventConverter))]
        public Event type { get; set; }
        public uint v { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public EventEntity eventEntity { get; set; }

        public List<byte> args { get; set; } // If eventEntity is null, use this

        public __dstEvent__()
        {
            type = Event.SetPlayerPosition;
            oldtype = type;
            v = 0;
            eventEntity = SB09WiiEventCaster.Cast(type);
            args = new();
        }

        public __dstEvent__(HoArchive.MemoryStreamEndian file, int nextptr) // Arglength needed because not all events are known
        {
            type = (Event)file.ReadUInt32E();
            oldtype = type;
            v = file.ReadUInt32E();

            file.Jump(v);
            eventEntity = SB09WiiEventCaster.Cast(type, file);
            file.Return();


            file.Jump(v);


            args = file.ReadBytes(nextptr - (int)v).ToList();
            

            file.Return();
            

        }

        public void Update()
        {
            if (oldtype != type)
            {
                eventEntity = SB09WiiEventCaster.Cast(type);
                args = new();
            }
            
            oldtype = type;

        }

        public void Save(HoArchive.MemoryStreamEndian file)
        {
            file.WriteE((uint)type);

            v = (uint)file.Position; // Procedure for writing pointers dynamically
            file.WriteE(0);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file)
        {
            uint offset = v;
            v = (uint)file.Position;
            file.Jump(offset);
            file.WriteE(v);
            file.Return();

            if (eventEntity != null) eventEntity.Save(file);
            else file.Write(args.ToArray());
        }
    }
}