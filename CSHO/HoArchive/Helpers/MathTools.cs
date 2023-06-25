using System;

namespace HoArchive{
    public class RGBA8888{
        public byte r {get; set;}
        public byte g {get; set;}
        public byte b {get; set;}
        public byte a {get; set;}

        public RGBA8888(byte r, byte g, byte b, byte a){
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public RGBA8888(MemoryStreamEndian file){
            r = file.ReadByte();
            g = file.ReadByte();
            b = file.ReadByte();
            a = file.ReadByte();
        }
        public void Save(MemoryStreamEndian file){
            file.WriteE(r);
            file.WriteE(g);
            file.WriteE(b);
            file.WriteE(a);
        }
    }


    public class float3{
        public float x {get; set;}
        public float y {get; set;}
        public float z {get; set;}

        public float3(BinaryReaderEndian file){ // Unused
            x = file.ReadFloat32E();
            y = file.ReadFloat32E();
            z = file.ReadFloat32E();
        }
        public float3(MemoryStreamEndian file){
            x = file.ReadFloat32E();
            y = file.ReadFloat32E();
            z = file.ReadFloat32E();
        }
        public float3(float x, float y, float z){
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(x);
            file.WriteE(y);
            file.WriteE(z);
        }
        public void Save(MemoryStreamEndian file){
            file.WriteE(x);
            file.WriteE(y);
            file.WriteE(z);
        }
    }

    public static class MathTools{
        public static uint RoundUpTo(uint num, uint up){
            return CeilDiv(num, up) * up;
        }
        public static uint CeilDiv(uint x, uint y){
            return ((x + y - 1) / y);
        }
        public static uint LowerCaseBKDR(string input){
            uint output = 0;
            foreach(char chr in input){
                output = LowerCase(Convert.ToByte(chr)) + output * 0x83;
            }
            return output;
        }
        public static byte LowerCase(byte chr){
            if (chr > 0x40 && chr < 0x5B){
                chr += 0x20;
            }
            return chr;
        }
    }
}