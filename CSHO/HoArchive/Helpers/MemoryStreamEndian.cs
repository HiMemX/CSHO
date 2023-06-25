using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoArchive{
    public class MemoryStreamEndian : MemoryStream{
        public bool endianness; // 0 = big, 1 = little
        public List<uint> returnAddresses = new List<uint>();
        // If function PostFix = "E", then it will read the data type according to the endianness

        public void Jump(uint offset){
            returnAddresses.Add((uint)Position);
            Position = offset;
        }

        public void Return(){
            Position = returnAddresses.Last();
            returnAddresses.RemoveAt(returnAddresses.Count-1);
        }

        public void Align(int align){
            Position = ((Position + align - 1) / align) * align;
        }

        public new byte ReadByte(){
            return (byte)base.ReadByte();
        }
        
        public byte[] ReadBytes(int amount){
            byte[] bytes = new byte[amount];
            for(int i=0; i<amount; i++){
                bytes[i] = (byte)ReadByte();
            }
            return bytes;
        }

        public byte[] ReadBytesE(int amount){
            byte[] value = ReadBytes(amount);
            if (!endianness)
                return value;
            return value.Reverse().ToArray();
        }

        public char[] ReadChars(int amount){
            char[] chars = new char[amount];
            for(int i=0;i<amount;i++){
                chars[i] = (char)ReadByte();
            }
            return chars;
        }

        public string ReadString(int stringlen){ // Big Endian
            return new string(ReadChars(stringlen)); 
        }
        
        public string ReadUntil(byte terminator){ // Big Endian
            byte curr_char;
            string output = "";
            while(true){
                curr_char = ReadByte();
                if (curr_char == terminator){break;}
                output += (char)curr_char;
            }
            return output;
        }

        public string ReadUntil(List<byte> terminators){
            byte curr_char;
            string output = "";
            while(true){
                curr_char = ReadByte();
                if (terminators.Contains(curr_char)){break;}
                output += (char)curr_char;
            }
            return output;
        }
        
        public ushort ReadUInt16(){
            return (ushort)(ReadByte() + (ReadByte() << 8));
        }
        public short ReadInt16(){
            return (short)(ReadByte() + (ReadByte() << 8));
        }
        public uint ReadUInt32(){
            return (uint)(ReadByte() + (ReadByte() << 8) + (ReadByte() << 16) + (ReadByte() << 24));
        }
        public int ReadInt32(){
            return ReadByte() + (ReadByte() << 8) + (ReadByte() << 16) + (ReadByte() << 24);
        }
        public ulong ReadUInt64(){
            return (ulong)(ReadByte() + (ReadByte() << 8) + (ReadByte() << 16) + (ReadByte() << 24) + (ReadByte() << 32) + (ReadByte() << 40) + (ReadByte() << 48) + (ReadByte() << 56));
        }


        public float ReadFloat32E(){
            uint value = ReadUInt32();
            if (endianness)
                return BitConverter.Int32BitsToSingle((int)value);
            return BitConverter.Int32BitsToSingle(BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0));
        }

        public MemoryStreamEndian(byte[] data, bool endian) : base(data){
            endianness = endian;
        }
        public MemoryStreamEndian(bool endian): base(){
            endianness = endian;
        }

        public ushort ReadUInt16E(){
            ushort value = ReadUInt16();
            if (endianness)
                return value;
            return BitConverter.ToUInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }
        public short ReadInt16E(){
            short value = ReadInt16();
            if (endianness)
                return value;
            return BitConverter.ToInt16(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        public uint ReadUInt32E(){
            uint value = ReadUInt32();
            if (endianness)
                return value;
            return BitConverter.ToUInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        public int ReadInt32E(){
            int value = ReadInt32();
            if (endianness)
                return value;
            return BitConverter.ToInt32(BitConverter.GetBytes(value).Reverse().ToArray(), 0);
        }

        public ulong ReadUInt64E(){
            ulong value1 = ReadUInt32E();
            ulong value2 = ReadUInt32E();
            if (endianness)
                return value1 + (value2 << 32);
            return (value1 << 32) + value2;
        }

        public bool ReadBool(){
            byte value = ReadByte();
            return value > 0;
        }


        public string ReadStringE(int stringlen){
            char[] chars = ReadChars(stringlen);
            if (endianness){
                Array.Reverse(chars);
            }
            return new string(chars);
        }

        public string ReadWideStringE(int maxlength){
            uint curr_char;
            string output = "";
            for (int i=0; i<maxlength; i++){
                curr_char = ReadUInt16E();
                output += curr_char.ToString();
            }
            return output;
        }
        public string ReadWideStringE(int maxlength, int terminator){
            ushort curr_char;
            string output = "";
            for (int i=0; i<maxlength; i++){
                curr_char = ReadUInt16E();
                if (curr_char == terminator){break;}
                output += (char)curr_char;
            }
            return output;
        }
        





        // Writing

        public void WriteString(string input){
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            Write(bytes);
        }

        public void WriteStringE(string input){
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            if (endianness){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteWideStringE(string input){
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte temp;
            if (endianness == false){
                for (int i=0; i<bytes.Count(); i += 2){
                    temp = bytes[i+1];
                    bytes[i+1] = bytes[i];
                    bytes[i] = temp;
                }
            }
            Write(bytes);
        }
        

        public void WriteE(byte[] input){
            byte[] bytes = input;
            if (endianness){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }
        

        public void WriteE(byte input){
            WriteByte(input);
        }
        
        public void WriteE(ushort input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteE(short input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteE(uint input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteE(ulong input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }

        public void WriteE(float input){
            byte[] bytes = BitConverter.GetBytes(input);
            if (endianness == false){
                bytes = bytes.Reverse().ToArray();
            }
            Write(bytes);
        }


        public void PadAlign(uint align, byte pad){
            PadTo(MathTools.RoundUpTo((uint)Position, align), pad);
        }

        public void Pad(uint amount, byte pad){
            for (int i=0; i<amount; i++){
                WriteByte(pad);
            }
        }

        public void PadTo(uint offset, byte pad){
            for (int i=(int)Position; i<offset; i++){
                WriteByte(pad);
            }
        }

        public void WriteE(bool input){
            if(input){WriteByte(1);}
            else{WriteByte(0);}
        }

    }
}