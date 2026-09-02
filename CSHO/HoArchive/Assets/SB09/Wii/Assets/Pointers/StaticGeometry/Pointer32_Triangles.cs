using System.Collections.Generic;

namespace SB09WiiAsset{
    public class Pointer32_Triangles : Pointer32{
        public List<Triangle> triangles {get;set;}

        public Pointer32_Triangles(List<Triangle> triangles){
            this.triangles = triangles;
        }

        public Pointer32_Triangles(HoArchive.MemoryStreamEndian file, uint count) : base(file){
            file.Jump(_p);
            triangles = new();
            for(int i=0; i<count; i++){
                triangles.Add(new Triangle(file));
            }

            file.Return();
        }

        public new void Update(){
        }

        public new void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);
            foreach(Triangle triangle in triangles){
                triangle.Save(file);
            }
        }
    }
}