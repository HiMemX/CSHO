namespace SB09WiiAsset{
    public class Matrix4x3{
        public float[,] matrix = new float[4, 3];

        public Matrix4x3(HoArchive.MemoryStreamEndian file){
            for(int y=0; y<3; y++){
                for(int x=0; x<4; x++){
                    matrix[y, x] = file.ReadFloat32E();
                }
            }
        }

        public Matrix4x3(){}
    
        public void Save(HoArchive.MemoryStreamEndian file){
            for(int y=0; y<3; y++){
                for(int x=0; x<4; x++){
                    file.WriteE(matrix[y, x]);
                }
            }
        }
    }
}