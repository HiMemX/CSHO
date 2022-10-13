using System;

namespace CSHO
{
    class Program
    {
        static void Main(string[] args)
        {  
            CSHO.Handler ArchiveHandler = new CSHO.Handler(); // Initialising your Handler
            
            string errorcode;
            errorcode = ArchiveHandler.NewFrom("S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SL01 Original.ho"); // Open an archive

            //Error Handeling
            if (errorcode != ""){ // Handler.Open() returns a string as an errorcode. It will return "" if the operation succeeded.
                Console.WriteLine("Exception: " + errorcode);
                Environment.Exit(1);
            }

            HoArchive.Table secttable = (HoArchive.Table)ArchiveHandler.Archive.MasterTable.Parcels[0]; // This is the table where all the layers are, like "P   ", "PD  " or "PTEX".
            HoArchive.Parcel parcel = (HoArchive.Parcel)secttable.Parcels[0]; // Here we select the first Parcel ("P   "). This also contains the 2 player assets from the original .ho file.
            ArchiveHandler.NewAsset(parcel.ParcelTOCs[0], 0x8008800880088008, 0x55555555, "C:\\Users\\felix\\Desktop\\test.dat"); // Here we pass it the first asset table, an assetid, a type and a path to a file we want to store.

            HoArchive.ParcelDebug debugparcel = (HoArchive.ParcelDebug)secttable.Parcels[1]; // Here we select the second Parcel ("PD  "), which is a Debug Parcel. It contains asset names.
            ArchiveHandler.NewNameTableEntry(debugparcel, 0x8008800880088008, "Test Asset!"); // Then we add a new name table entry to add the assets name.
            // This casting is needed because because both parceltypes (Parcel and ParcelDebug) share the same list, thus the compiler needs to know what parceltype is being worked on right now.


            ArchiveHandler.path = "C:\\Users\\felix\\Desktop\\CSHO.ho";//"S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SL01.ho";//
            ArchiveHandler.Save();
            //*/
        }
    }
}