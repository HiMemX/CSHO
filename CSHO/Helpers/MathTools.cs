
static class MathTools{
    public static uint RoundUpTo(uint num, uint up){
        return CeilDiv(num, up) * up;
    }
    public static uint CeilDiv(uint x, uint y){
        return ((x + y - 1) / y);
    }
}