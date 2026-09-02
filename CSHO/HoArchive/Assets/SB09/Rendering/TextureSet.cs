namespace RenderingInternal{
    public class TextureSet{
        public GenericBuffer diffuseMap = new();
        public GenericBuffer lightMap = new();

        public ulong diffuseMapID = 0;
        public ulong lightMapID = 0;
    }
}