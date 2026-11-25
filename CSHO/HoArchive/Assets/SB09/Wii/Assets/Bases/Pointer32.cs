using System;
using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32{
        public uint _p;

        public Pointer32(){
            _p = 0;
        }

        public Pointer32(HoArchive.MemoryStreamEndian file){
            _p = file.ReadUInt32E();
        }

        // Common codec-based list operations. The caller still owns the header,
        // null/zero-pointer policy, alignment and any second-pass heap writes.
        protected List<T> ReadItems<T>(HoArchive.MemoryStreamEndian file, uint count,
            Func<HoArchive.MemoryStreamEndian, T> readItem){
            file.Jump(_p);
            List<T> items = new();
            for(int i=0; i<count; i++){
                items.Add(readItem(file));
            }
            file.Return();
            return items;
        }

        protected void SaveItems<T>(HoArchive.MemoryStreamEndian file, List<T> items,
            Action<HoArchive.MemoryStreamEndian, T> writeItem){
            Save(file); // Patch the previously reserved 32-bit offset.
            foreach(T item in items){
                writeItem(file, item);
            }
        }

        public void Update(){
        }

        public void SavePointer(HoArchive.MemoryStreamEndian file){
            _p = (uint)file.Position;
            file.WriteE((uint)0);
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            uint offset = (uint)file.Position;
            file.Jump(_p);
            file.WriteE(offset);
            file.Return();
            _p = offset;
        }
    }
}