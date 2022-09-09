
namespace HoArchive{
    public struct float3{
        public float x;
        public float y;
        public float z;
    }

    public static class MathTools{
        public static uint RoundUpTo(uint num, uint up){
            return CeilDiv(num, up) * up;
        }
        public static uint CeilDiv(uint x, uint y){
            return ((x + y - 1) / y);
        }
    }
}