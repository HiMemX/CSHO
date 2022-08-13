using System;

namespace CSHO
{
    class Program
    {
        static void Main(string[] args)
        {  
            CSHO.Handler Handler = new CSHO.Handler(); // Initialising your Handler
            
            string errorcode;
            errorcode = Handler.Open("C:\\Users\\felix\\Desktop\\SHUB.ho"); // Open an archive

            // Error Handeling
            if (errorcode != ""){ // Handler.Open() returns a string as an errorcode. It will return "" if the operation succeeded.
                Console.WriteLine("Exception: " + errorcode);
                Environment.Exit(1);
            }

            byte[] id = new byte[8] {0x00, 0x00, 0x01, 0x03, 0x00, 0x00, 0x00, 0x2E}; // Specifying an AssetID (AssetIDs are byte arrays)
            HoArchive.TOCEntry asset = Handler.GetAsset(id); // Get Asset
            HoArchive.NameTableEntry nametableentry = Handler.GetNameEntry(id); // Get NameTableEntry (From Debug Parcels)


            Console.WriteLine("GENERAL PACKAGE INFORMATION");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Created: " + Handler.Archive.Header.timeString);
            Console.WriteLine("Sector Size: " + Handler.Archive.Header.sectorSize);
            Console.WriteLine("Platform: " + Handler.Archive.Header.platform);
            Console.WriteLine("Target: " + Handler.Archive.Header.target);
            Console.WriteLine("User: " + Handler.Archive.Header.user);
            Console.WriteLine("Creator: " + Handler.Archive.Header.creator);
            Console.WriteLine();
            Console.WriteLine("RANDOM ASSET INFORMATION");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Asset: " + nametableentry.name);
            Console.WriteLine("ID: " + Convert.ToHexString(asset.uidSelf));
            Console.WriteLine("Element Size: " + asset.elementSize);
            Console.WriteLine("Element Offset: " + asset.elementOffset);
            Console.WriteLine("Blob Size: " + asset.blobSize);
            Console.WriteLine("Type: " + Convert.ToHexString(asset.wmlTypeID));
            
            // Modify your archive
            Handler.Archive.Header.user = "Your name here!";
            Handler.Archive.Header.comment = "Your comment here!";
            Handler.Archive.Header.creator = "CSHO baby";

            // Save it. No Update() needed! (Jk it's done by the handler but shhh)
            Handler.Save();


            /*
            Extra Information:
            You'll find everything Handler related in the CSHO folder.
            CSHO.cs -> Declares variables
            Tools.cs -> Contains Get() and Set() methods
                |-> Get() will return null if it hasn't found what you're looking for
                |-> Set() will always return a string as an errorcode, empty if success
            New.cs -> Future New Archive generation
            Open.cs -> Methods for opening an Archive
            Save.cs -> Methods for saving an Archive

            All the HoLib stuff is in the HoArchive folder.
            The code might be a bit hard to read since I'm not experienced with C#.
            */
        }
    }
}
