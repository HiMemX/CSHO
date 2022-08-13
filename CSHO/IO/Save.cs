using System.IO;

namespace CSHO{
    public partial class Handler{
        public string Save(){
            HoArchive.BinaryWriterEndian file;
            
            try{file = new HoArchive.BinaryWriterEndian(path, endian);}
            catch(FileNotFoundException){ return "ERR_FILE_NOT_FOUND"; }
            catch(DirectoryNotFoundException) { return "ERR_DIRECTORY_NOT_FOUND";}
            catch(IOException)          { return "ERR_IO_EXCEPTION"; }

            Archive.Update();
            Archive.Save(file);

            return "";
        }
    }
}