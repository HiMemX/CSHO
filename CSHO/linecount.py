import os
rootdir = r"E:\CSHO"

count = 0
for subdir, dirs, files in os.walk(rootdir):
    for file in files:
        path = os.path.join(subdir, file)
        
        if path.split(".")[-1] == "cs":
            with open(path, "r") as file: count += len(file.readlines())

print(count)