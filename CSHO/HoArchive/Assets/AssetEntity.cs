using System;
using System.Collections.Generic;

namespace Asset{
    public abstract class AssetEntity{

        public abstract void Save(HoArchive.MemoryStreamEndian file);
        public virtual void Update(HoArchive.TOCEntry entry){}
    }
}