using System;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace HoArchive{
    public class Header{
        [ReadOnly(true)]
        public string cMagic {get; set;} // Necessary
        public uint verPackFile {get; set;}
        public uint verWMlSchema {get; set;}
        public uint geBuildNum {get; set;}
        [ReadOnly(true)]
        public ulong timeValue {get; set;}
        [ReadOnly(true)]
        public string timeString {get; set;}
        [TypeConverter(typeof(UIntConverter))]
        public uint sectorSize {get; set;}
        [ReadOnly(true)]
        public uint startSector {get; set;}
        [ReadOnly(true)]
        public uint tableSize {get; set;}
        public uint pad01 {get; set;}
        public string platform {get; set;}
        public string user {get; set;}
        public string target {get; set;}
        public string creator {get; set;}
        public string comment {get; set;}
        public string hash {get; set;}
        public uint libVersion {get; set;}
        public uint pad02 {get; set;}

        public Header(
            string cMagic_in,
            string platform_in,
            string target_in,
            uint sectorSize_in = 0x800,
            string user_in = "csho_autobuild",
            string creator_in = "",
            string comment_in = "",
            string hash_in = ""
        ){
            cMagic = cMagic_in;
            verPackFile = 1;
            verWMlSchema = 0;
            geBuildNum = 1;
            timeValue = 0;
            timeString = "";
            sectorSize = sectorSize_in;
            startSector = 1;
            tableSize = 0;
            pad01 = 0;
            platform = platform_in;
            user = user_in;
            target = target_in;
            creator = creator_in;
            comment = comment_in;
            hash = hash_in;
            libVersion = 1;
            pad02 = 0;

        }

        public Header(BinaryReaderEndian file){
            cMagic       = file.ReadString(0x04);
            verPackFile  = file.ReadUInt32E();
            verWMlSchema = file.ReadUInt32E();
            geBuildNum   = file.ReadUInt32E();
            timeValue    = file.ReadUInt64E();
            timeString   = file.ReadString(0x19);

            file.BaseStream.Position = 0x40;

            sectorSize   = file.ReadUInt32E();
            startSector  = file.ReadUInt32E();
            tableSize    = file.ReadUInt32E();
            pad01        = file.ReadUInt32E();

            file.BaseStream.Position = 0x400;
            platform = file.ReadWideStringE(0x06, 0);
            file.BaseStream.Position = 0x43C;
            user     = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x47C;
            target   = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x4BC;
            creator  = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x4FC;
            comment  = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x5FC;
            hash     = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x63C;

            libVersion = file.ReadUInt32E();
            pad02      = file.ReadUInt32E();
        }

        public void Update(Table MasterTable){
            cMagic = StringTools.ConditionalTrim(cMagic, 0x04);
            timeValue = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
            timeString = DateTime.Now.ToString("ddd MMM dd HH:mm:ss yyyy", CultureInfo.GetCultureInfo("en-US")) + "\n";

            tableSize = MathTools.RoundUpTo((uint)MasterTable.TableEntries.Count*0x40 + 0x20 + MasterTable.TableHeader.stringTableSize, 0x20);

            platform = StringTools.ConditionalTrim(platform, 0x20);
            user     = StringTools.ConditionalTrim(user, 0x20);
            target   = StringTools.ConditionalTrim(target, 0x20);
            creator  = StringTools.ConditionalTrim(creator, 0x20);
            comment  = StringTools.ConditionalTrim(comment, 0x20);
            hash     = StringTools.ConditionalTrim(hash, 0x20);
        }

        public void Save(BinaryWriterEndian file){
            file.WriteString(cMagic);
            file.WriteE(verPackFile);
            file.WriteE(verWMlSchema);
            file.WriteE(geBuildNum);
            file.WriteE(timeValue);
            file.WriteString(timeString);
            file.PadTo(0x40, 0x00);
            file.WriteE(sectorSize);
            file.WriteE(startSector);
            file.WriteE(tableSize);
            file.WriteE(pad01);
            file.PadTo(0x400, 0x00);

            file.WriteWideStringE(platform);
            file.PadTo(0x43C, 0x00);
            file.WriteWideStringE(user);
            file.PadTo(0x47C, 0x00);
            file.WriteWideStringE(target);
            file.PadTo(0x4BC, 0x00);
            file.WriteWideStringE(creator);
            file.PadTo(0x4FC, 0x00);
            file.WriteWideStringE(comment);
            file.PadTo(0x5FC, 0x00);
            file.WriteWideStringE(hash);
            file.PadTo(0x63C, 0x00);

            file.WriteE(libVersion);
            file.WriteE(pad02);
        }

        public void SaveLSET(StreamWriter file, string indent){
            file.WriteLine(indent + "Header{");
            file.WriteLine(indent + "   magic     =" + cMagic);            
            file.WriteLine(indent + "   sectorSize=" + sectorSize.ToString());
            file.WriteLine();
            file.WriteLine(indent + "   platform  =" + platform);
            file.WriteLine(indent + "   user      =" + user);
            file.WriteLine(indent + "   target    =" + target);
            file.WriteLine(indent + "   creator   =" + creator);
            file.WriteLine(indent + "   comment   =" + comment);
            file.WriteLine(indent + "   hash      =" + hash);
            file.WriteLine(indent + "}");
        }

        public Header(List<string> args){
            cMagic = "HEL→";
            verPackFile = 1;
            verWMlSchema = 0;
            geBuildNum = 1;
            timeValue = 0;
            timeString = "";
            sectorSize = 0x800;
            startSector = 1;
            tableSize = 0;
            pad01 = 0;
            platform = "WII";
            user = "csho_autobuild";
            target = "SB09";
            creator = "";
            comment = "";
            hash = "7989bbee658a35f8701478eb9f0909d3";
            libVersion = 1;
            pad02 = 0;

            List<string> elements;
            string field;
            string value;
            foreach(string arg in args){
                if(arg.Split("=").Length == 0){continue;}
                elements = arg.Split("=").ToList();
                field = elements[0];
                elements.RemoveAt(0);
                value = String.Join("=", elements);
                
                if(field == "magic"){cMagic = value;}
                if(field == "sectorSize"){sectorSize = uint.Parse(value);}
                
                if(field == "platform"){platform = value;}
                if(field == "user"){user = value;}
                if(field == "target"){target = value;}
                if(field == "creator"){creator = value;}
                if(field == "comment"){comment = value;}
                if(field == "hash"){hash = value;}
            }
        }
    }
}