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

            ulong id = 0x0001E0BA0000E778;
            HoArchive.TOCEntry asset = Handler.GetAsset(id); // Get Asset

            if (asset == null){
                Console.WriteLine("Asset not found!");
                Environment.Exit(1);
            }

            Console.WriteLine(BitConverter.ToString(((SB09Assets.SimpleObject)asset.entity).EventLinksNew.EventLinksArray.events.ToArray())); // 
            
        }
    }
}