using System;

namespace CSHO
{
    class Program
    {
        static void Main(string[] args)
        {  
            CSHO.Handler ArchiveHandler = new CSHO.Handler(); // Initialising your Handler
            
            string errorcode;
            errorcode = ArchiveHandler.NewFrom("S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SHUB Original.ho"); // Open an archive

            //Error Handeling
            if (errorcode != ""){ // Handler.Open() returns a string as an errorcode. It will return "" if the operation succeeded.
                Console.WriteLine("Exception: " + errorcode);
                Environment.Exit(1);
            }

            Console.WriteLine(ArchiveHandler.Archive.MasterTable.TableEntries[0].parcelType.ToString());

            //ArchiveHandler.NewAsset(parcel.ParcelTOCs[0], 0x8008800880088008, 0x55555555, "C:\\Users\\felix\\Desktop\\test.dat"); // Here we pass it the first asset table, an assetid, a type and a path to a file we want to store.

            //HoArchive.ParcelDebug debugparcel = (HoArchive.ParcelDebug)secttable.Parcels[1]; // Here we select the second Parcel ("PD  "), which is a Debug Parcel. It contains asset names.
            //ArchiveHandler.NewNameTableEntry(debugparcel, 0x8008800880088008, "Test Asset!"); // Then we add a new name table entry to add the assets name.
            // This casting is needed because because both parceltypes (Parcel and ParcelDebug) share the same list, thus the compiler needs to know what parceltype is being worked on right now.


            //ArchiveHandler.path = "S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SHUB.ho";//"C:\\Users\\felix\\Desktop\\CSHO.ho";//
            //ArchiveHandler.Save();
            //*/
        }
    }
}