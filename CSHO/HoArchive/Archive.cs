using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace HoArchive{
    public class Archive{
        public Header Header;
        public Table MasterTable;

        public Archive(
            string cMagic_in,
            string platform_in,
            string target_in,
            string DomainString, 
            uint sectorSize_in = 0x800,
            string user_in = "csho_autobuild",
            string creator_in = "",
            string comment_in = "",
            string hash_in = "",
            uint tableFlags = 0,
            string tableTypeTag = "MAST"
        ){
            Header = new Header(cMagic_in, platform_in, target_in, sectorSize_in, user_in, creator_in, comment_in, hash_in);
            MasterTable = new Table(tableTypeTag, DomainString, tableFlags);
        }

        public Archive(BinaryReaderEndian file){
            Header = new Header(file);

            file.BaseStream.Position = Header.startSector * Header.sectorSize;
            MasterTable = new Table(file, Header.sectorSize, Header.target, Header.platform);
        }

        public void Update(){
            uint startSector  = MathTools.CeilDiv(0x800, Header.sectorSize);
            Header.startSector = startSector;

            MasterTable.Update(0x40, Header.sectorSize, false); // 0x40 is default alignment
            MasterTable.updateSectors(Header.sectorSize, startSector);
            Header.Update(MasterTable);
        }

        public void Save(BinaryWriterEndian file){
            Header.Save(file);
            
            file.PadTo(Header.sectorSize*Header.startSector, 0x00);

            MasterTable.Save(file, Header.sectorSize, new List<SliceMeta>());
        }

        public void SaveLSET(StreamWriter file, List<NameTableEntry> nameTableEntries){
            Header.SaveLSET(file, "");

            file.WriteLine();

            MasterTable.SaveLSET(file, "", "MAST", nameTableEntries);
        }

        public Archive(StreamReader file, string assetpath){ // LSET Tomfoolery
            file.BaseStream.Position = 0;
            List<string> lines = StringTools.ReadLines(file);
            lines = StringTools.RemoveOverheadSpaces(lines);

            List<string> headerLines = new List<string>();
            bool gotHeader = false;
            foreach(string line in lines){
                if(gotHeader){
                    if(line == "}"){break;}
                    headerLines.Add(line.Replace(" ", ""));
                    continue;
                }
                if(line.Length < 7){continue;}
                if(line.Substring(0, 7) != "Header{"){continue;}
                gotHeader = true;
            }
            Header = new Header(headerLines);
            
            List<string> masterTableLines = new List<string>();
            List<string> masterTableArgs = new List<string>();
            bool gotMaster = false;
            foreach(string line in lines){
                if(gotMaster){
                    masterTableLines.Add(line);
                }
                if(line.Length < 5){continue;}
                if(line.Substring(0, 5) != "MAST("){continue;}
                gotMaster = true;
                masterTableArgs = StringTools.GetArgs(line);

            }
            masterTableLines = StringTools.ReadUntilCloseBracket(masterTableLines);
            uint tableFlags = 0;
            string arg = StringTools.GetArg(masterTableArgs, "tableFlags");
            if(arg != null){tableFlags = uint.Parse(arg);}
            MasterTable = new Table(masterTableLines, "MAST", Header.target, Header.platform, assetpath, tableFlags);

            //Console.WriteLine(String.Join("\n", masterTableLines));
            //Console.WriteLine(String.Join("\n", masterTableArgs));;
        }


    }
}