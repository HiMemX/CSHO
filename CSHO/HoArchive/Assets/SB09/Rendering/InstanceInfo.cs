using System.Numerics;

namespace RenderingInternal{
    public struct InstanceInfo
    {
        public Matrix4x4 matrix;
        public uint flags;

        public uint parentAttr;
        public uint childAttr;

        public int lightkitIndex;

        public InstanceInfo Clone()
        {
            InstanceInfo info = new InstanceInfo();
            info.matrix = matrix;
            info.flags = flags;
            info.parentAttr = parentAttr;
            info.childAttr = childAttr;
            info.lightkitIndex = lightkitIndex;

            return info;
        }
    }
}