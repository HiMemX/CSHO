namespace RenderingInternal{
    public class ChannelInfo{ // Most important information
        public int vtype;
        public int vindex;
        public int vfrac;
        public int voffset;
        public int vstride;

        public int GetTupleCount()
        {
            switch(vtype){
                case 1:
                    return 3;
                case 3:
                    return 2;
                case 4:
                    return 3;
                case 5:
                    return 4;
                default:
                    return 0;
            }
        }
    }
}