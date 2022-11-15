using System.IO;

namespace CSHO{
    public partial class Handler{
        public string Open(string path_in){
            HoArchive.BinaryReaderEndian file = null;
            HoArchive.Archive temparchive;

            try{file = new HoArchive.BinaryReaderEndian(path_in, false);} // Using false as default endian, will change on FetchEndian
            catch(FileNotFoundException){ file.Dispose();  return "ERR_FILE_NOT_FOUND"; }
            catch(DirectoryNotFoundException) { file.Dispose();  return "ERR_DIRECTORY_NOT_FOUND";}
            catch(IOException)          { file.Dispose();  return "ERR_IO_EXCEPTION"; }

            if (FetchValid(file) == false){ file.Dispose();  return "ERR_INVALID_FILE"; }
            
            endian = FetchEndian(file);
            file.endianness = endian;

            try{temparchive = new HoArchive.Archive(file);}
            catch(EndOfStreamException){ file.Dispose();  return "ERR_END_OF_STREAM"; }
            
            Archive = temparchive;
            path = path_in;

            file.Dispose();
            return "";
        }
        
    }
}