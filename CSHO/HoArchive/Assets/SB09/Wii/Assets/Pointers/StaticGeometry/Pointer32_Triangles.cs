using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Triangles : Pointer32{
        public List<Triangle> triangles {get;set;}

        public Pointer32_Triangles() : base(){
            triangles = new();
        }

        public Pointer32_Triangles(List<Triangle> triangles){
            this.triangles = triangles;
        }

        public Pointer32_Triangles(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            triangles = ReadItems<Triangle>(file, count, stream => new Triangle(stream));
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            SaveItems(file, triangles, (stream, item) => item.Save(stream));
        }
    }
}