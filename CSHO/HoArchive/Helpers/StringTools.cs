using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HoArchive{
    public static class StringTools{
        static Regex rgx = new Regex("[^a-zA-Z0-9 -]");

        public static string ConditionalTrim(string input, int length){
            if (input.Length > length){input = input.Remove(length);}
            return input;
        }
        

        public static bool LetterTokenCheck(string check, string token){
            return rgx.Replace(check, " ").Split(" ").ToList().Contains(token);
        }
        public static bool TokenCheck(string check, string token){
            return check.Contains(token);
        }


        public static List<string> ReadLines(StreamReader file){
            List<string> output = new List<string>();

            while(!file.EndOfStream){
                output.Add(file.ReadLine());
            }
            return output;
        }

        public static List<string> RemoveOverheadSpaces(List<string> lines){
            List<string> output = new List<string>();
            string newline;
            bool gotChar;
            foreach(string line in lines){
                newline = "";
                gotChar = false;
                foreach(char chr in line){
                    if(!gotChar && chr == ' '){continue;}
                    newline += chr;
                    gotChar = true;
                }
                output.Add(newline);
            }
            return output;
        }
        public static List<string> GetArgs(string line){
            string args = line.Replace(")", "(").Split("(")[1].Replace(" ", "");
            return args.Split(",").ToList();
        }

        public static List<string> ReadUntilCloseBracket(List<string> lines){
            int pCount = 1;
            List<string> output = new List<string>();

            foreach(string line in lines){
                if(line.Contains("{")){pCount++;}
                if(line.Contains("}")){pCount--;}
                if(line == "}" && pCount == 0){break;}
                output.Add(line);
            }
            return output;
        }

        public static string GetArg(List<string> args, string field){
            foreach(string arg in args){
                if(arg.Split("=")[0] == field){
                    return arg.Split("=")[1];
                }
            }
            return null;
        }

        public static string RemovePaddingSpaces(string line){
            line = line.Replace("	", " ");
            string output = "";
            char lastchar = ' ';
            foreach(char chr in line){
                if(lastchar == ' ' && chr == ' '){continue;}
                lastchar = chr;
                output += chr;
            }
            return output;
        }
    }
}