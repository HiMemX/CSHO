using System.ComponentModel;


// Why
// Why Energy


namespace SB09WiiAsset{
    public class MaterialParam{
        public byte type {get; set;}
        public byte debugIndex {get; set;}
        public ushort elementCount {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pointer32 __anon {get; set;}
        
        public MaterialParam(){
            __anon = new Pointer32_f();
        }

        public MaterialParam(HoArchive.MemoryStreamEndian file){
            type = file.ReadByte();
            debugIndex = file.ReadByte();
            elementCount = file.ReadUInt16E();

            if(type == 0){ __anon = new Pointer32_f(file, elementCount); }
            if(type == 1){ __anon = new Pointer32_i(file, elementCount); }
            if(type == 2){ __anon = new Pointer32_b(file, elementCount); }
            if(type == 3){ __anon = new Pointer32_SamplerParamData(file, elementCount); }
        }

        public void Update(){
            if(type == 0 && __anon is not Pointer32_f){ __anon = new Pointer32_f(); }
            if(type == 1 && __anon is not Pointer32_i){ __anon = new Pointer32_i(); }
            if(type == 2 && __anon is not Pointer32_b){ __anon = new Pointer32_b(); }
            if(type == 3 && __anon is not Pointer32_SamplerParamData){ __anon = new Pointer32_SamplerParamData(); }

            switch(type){
                case 0:
                    elementCount = (ushort)((Pointer32_f)__anon).f.Count;
                    break;
                case 1:
                    elementCount = (ushort)((Pointer32_i)__anon).i.Count;
                    break;
                case 2:
                    elementCount = (ushort)((Pointer32_b)__anon).b.Count;
                    break;
                case 3:
                    elementCount = (ushort)((Pointer32_SamplerParamData)__anon).samp.Count;
                    break;
            }
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(type);
            file.WriteE(debugIndex);
            file.WriteE(elementCount);
            __anon.SavePointer(file);
        }

        public void SaveHeap(HoArchive.MemoryStreamEndian file){
            switch(type){
                case 0:
                    ((Pointer32_f)__anon).Save(file);
                    break;
                case 1:
                    ((Pointer32_i)__anon).Save(file);
                    break;
                case 2:
                    ((Pointer32_b)__anon).Save(file);
                    break;
                case 3:
                    ((Pointer32_SamplerParamData)__anon).Save(file);
                    break;
            }
        }
    }
}