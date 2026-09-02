import math

with open("SB09Assets.txt", "r") as file:
    events = [[i.split(" = ")[0], int(i.split(" = ")[1], 16)] for i in file.read().split("\n")]


def sort_events(events):
    for i in range(1, len(events)):
        for k in reversed(range(1, i+1)):
            if events[k][1] >= events[k-1][1]: continue

            events[k], events[k-1] = events[k-1], events[k]

    return events

def get_split_value(ls):
    if type(ls[0]) is str: return ls[1]
    return get_split_value(ls[0])

def binary_treeify(ls):
    if len(ls) == 1: return ls[0]
    if len(ls) == 2: return ls

    pivot = math.ceil(len(ls) / 2)

    ls1 = binary_treeify(ls[:pivot])
    ls2 = binary_treeify(ls[pivot:])

    return [ls1, ls2]

def generate_code_block(ls, indent_level=0):
    if len(ls) == 1: return indent_level * "    " + f"return new {ls[0][0]}(file);"

    pivot = math.ceil(len(ls) / 2)

    ls1 = ls[:pivot]
    ls2 = ls[pivot:]

    split_value = ls2[0][0]

    indents = indent_level * "    "

    line1 = indents + f"if (wmlTypeID < HoArchive.wmlTypeID.{split_value})" + "{\n"
    lines2 = generate_code_block(ls1, indent_level + 1) + "\n"
    line3 = indents + "}\n"
    lines4 = generate_code_block(ls2, indent_level)

    return  line1 + lines2 + line3 + lines4

#if(wmlTypeID == HoArchive.wmlTypeID.AnimationSet){return new AnimationSet(file);}
events = sort_events(events)
events = events#[:4]
print(events)
#events = binary_treeify(events)
# Construct tree
print(generate_code_block(events))
#print(get_split_value(events[1]))
exit()

for event in events:
    i = event[0]
    print(event)
    #print(f"if(wmlTypeID == HoArchive.wmlTypeID.{i})" + "{return new " + i + "(file);}")

'''
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
        file.write(code)'''
