using System;
using System.Collections.Generic;

namespace Asset{
    public abstract class EventEntity{

        public abstract void Save(HoArchive.MemoryStreamEndian file);
    }
}