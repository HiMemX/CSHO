using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;

namespace HoArchive{
    public class Matrix4x3{
        //[TypeConverter(typeof(Expand))]
        public float[,] matrix {get;set;} = new float[4, 3];

        public Matrix4x3(HoArchive.MemoryStreamEndian file){
            for(int y=0; y<3; y++){
                for(int x=0; x<4; x++){
                    matrix[x, y] = file.ReadFloat32E();
                }
            }
        }

        public Matrix4x3(){}

        public void Identity(){
            for(int i=0; i<3; i++){
                matrix[i,i] = 1;
            }
        }

        public void Save(HoArchive.MemoryStreamEndian file){
            for(int y=0; y<3; y++){
                for(int x=0; x<4; x++){
                    file.WriteE(matrix[x, y]);
                }
            }
        }

        public void SetTranslation(Vector3 translation){
            matrix[3, 0] = translation.X;
            matrix[3, 1] = translation.Y;
            matrix[3, 2] = translation.Z;
        }

        public Matrix4x4 GetSystemMatrix(){ // Matrix if applied to a row vector
            return new Matrix4x4(
                matrix[0,0],  matrix[0,1],  matrix[0,2],  0,
                matrix[1,0],  matrix[1,1],  matrix[1,2],  0 ,
                matrix[2,0],  matrix[2,1],  matrix[2,2],  0, 
                matrix[3,0],  matrix[3,1],  matrix[3,2],  1
            );
        }

        public Matrix4x3(Matrix4x4 mat){
            matrix[0,0] = mat.M11;
            matrix[0,1] = mat.M12;
            matrix[0,2] = mat.M13;

            matrix[1,0] = mat.M21;
            matrix[1,1] = mat.M22;
            matrix[1,2] = mat.M23;

            matrix[2,0] = mat.M31;
            matrix[2,1] = mat.M32;
            matrix[2,2] = mat.M33;

            matrix[3,0] = mat.M41;
            matrix[3,1] = mat.M42;
            matrix[3,2] = mat.M43;
        }
    }


    public class FloatColorRGB{
        public float r {get; set;}
        public float g {get; set;}
        public float b {get; set;}

        public FloatColorRGB(float r, float g, float b){
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public FloatColorRGB(MemoryStreamEndian file){
            r = file.ReadFloat32E();
            g = file.ReadFloat32E();
            b = file.ReadFloat32E();
        }
        public void Save(MemoryStreamEndian file){
            file.WriteE(r);
            file.WriteE(g);
            file.WriteE(b);
        }
    }

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

    public class float4{
        public float x {get; set;}
        public float y {get; set;}
        public float z {get; set;}
        public float w {get; set;}

        public float4(BinaryReaderEndian file){ // Unused
            x = file.ReadFloat32E();
            y = file.ReadFloat32E();
            z = file.ReadFloat32E();
            w = file.ReadFloat32E();
        }
        public float4(MemoryStreamEndian file){
            x = file.ReadFloat32E();
            y = file.ReadFloat32E();
            z = file.ReadFloat32E();
            w = file.ReadFloat32E();
        }
        public float4(float x, float y, float z, float w){
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(x);
            file.WriteE(y);
            file.WriteE(z);
            file.WriteE(w);
        }
        public void Save(MemoryStreamEndian file){
            file.WriteE(x);
            file.WriteE(y);
            file.WriteE(z);
            file.WriteE(w);
        }

        
    }

    public class float3{
        public float x {get; set;}
        public float y {get; set;}
        public float z {get; set;}

        public float3(Vector3 vec){
            x = vec.X;
            y = vec.Y;
            z = vec.Z;
        }

        public float3(){
        }

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

        public Vector3 GetVector3(){
            return new Vector3(x,y,z);
        }
    }

    public static class MathTools{
        public static uint RoundDownTo(uint num, uint down){
            return (num / down) * down;
        }
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