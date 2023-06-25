
with open("SB09Assets.txt", "r") as file:
    events = [[i.split(" = ")[0], int(i.split(" = ")[1], 16)] for i in file.read().split("\n")]


#if(wmlTypeID == HoArchive.wmlTypeID.AnimationSet){return new AnimationSet(file);}
#for event in events:
#    i = event[0]
#    print(f"if(wmlTypeID == HoArchive.wmlTypeID.{i})" + "{return new " + i + "(file);}")


for event in events:
    code="""namespace SB09WiiAsset{
    public class """ + event[0] + """ : Asset.AssetEntity{
        public """ + event[0] + """(HoArchive.MemoryStreamEndian file){
        }

        public override void Update(HoArchive.TOCEntry entry){
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
        }
    }
}"""
    with open(f"Output\\{event[0]}.cs", "w+") as file:
        file.write(code)
