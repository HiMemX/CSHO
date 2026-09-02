
using System.ComponentModel;
using System.Collections.Generic;

namespace SB09WiiAsset{
    public class CollKDTree{
        public uint branchCount {get;set;}
        public Pointer32_Branches branchList;
        public List<Branch> _branchList {get {return branchList.branches;} set{branchList.branches = value;}}

        public uint triangleCount {get;set;}
        public Pointer32_Triangles triangleList;
        public List<Triangle> _triangleList {get {return triangleList.triangles;} set{triangleList.triangles = value;}}

        public CollKDTree(){
            branchList = new Pointer32_Branches(new List<Branch>());
            triangleList = new Pointer32_Triangles(new List<Triangle>());
        }

        public CollKDTree(HoArchive.MemoryStreamEndian file){
            file = file.NewStreamFromHere(); // Pointers are relative to start from this
            branchCount = file.ReadUInt32E();
            branchList = new Pointer32_Branches(file, branchCount);
            triangleCount = file.ReadUInt32E();
            triangleList = new Pointer32_Triangles(file, triangleCount);
        }

        public void Update(){
            branchCount = (uint)branchList.branches.Count;
            triangleCount = (uint)triangleList.triangles.Count;
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            

            HoArchive.MemoryStreamEndian temp = new HoArchive.MemoryStreamEndian(file.endianness);
            temp.WriteE(branchCount);
            branchList.SavePointer(temp);
            temp.WriteE(triangleCount);
            triangleList.SavePointer(temp);

            branchList.Save(temp);
            triangleList.Save(temp);

            file.WriteE(temp.ToArray());
        }
    }
}