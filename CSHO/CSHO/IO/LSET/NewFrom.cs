using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CSHO{
    public partial class Handler{
        public string NewFromLSET(string path){ 
            StreamReader file = null;

            try{file = new StreamReader(path);} // Using false as default endian, will change on FetchEndian
            catch(FileNotFoundException){ file.Dispose();  return "ERR_FILE_NOT_FOUND"; }
            catch(DirectoryNotFoundException) { file.Dispose();  return "ERR_DIRECTORY_NOT_FOUND";}
            catch(IOException)          { file.Dispose();  return "ERR_IO_EXCEPTION"; }

            List<string> segments = path.Replace("\\", "/").Split("/").ToList();
            segments.RemoveAt(segments.Count-1);
            Archive = new HoArchive.Archive(file, String.Join("/", segments) + "/Assets");

            return "";
        }
    }
}