using System.IO;

namespace CSHO{
    public partial class Handler{
        public string Save(bool update = true){
            if(path == ""){return "PATH_NOT_SET";}
            if(Archive == null){return "ARCHIVE_NOT_SET";}

            HoArchive.BinaryWriterEndian file = null;
            
            try{file = new HoArchive.BinaryWriterEndian(path, endian);}
            catch(FileNotFoundException){ file.Dispose(); return "ERR_FILE_NOT_FOUND"; }
            catch(DirectoryNotFoundException) { file.Dispose();  return "ERR_DIRECTORY_NOT_FOUND";}
            catch(IOException)          { file.Dispose();  return "ERR_IO_EXCEPTION"; }

            if(update){Archive.Update();}
            Archive.Save(file);

            file.Dispose();

            return "";
        }

        public string Update(){
            if(Archive == null){return "ARCHIVE_NOT_SET";}

            foreach(HoArchive.NameTableEntry entry in GetNameEntries()){
                if(GetAsset(entry.uidAsset) == null){
                    entry.delete = true;
                }
            }

            foreach (HoArchive.ParcelBase parcel in GetParcels()){
                if (parcel is HoArchive.ParcelDebug == false){continue;}

                if(((HoArchive.ParcelDebug)parcel).NameTableEntries.Count == 0){((HoArchive.ParcelDebug)parcel).delete = true;}
            }

            Archive.Update();
            return "";
        }
    }
}