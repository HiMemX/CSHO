

using System;
using System.Collections.Generic;
using CSHO;
using HoArchive;

namespace Example;

public static class PrintAssetCount
{
    public static void Run(string path)
    {
        /// <summary>
        ///  Print out number of assets in file
        /// </summary>
        
        Handler handler = new Handler(); // Initialize Handler.
        handler.Open(path); // Open .ho archive.

        List<TOCEntry> assets = handler.GetAssets();
        int count = assets.Count;

        Console.WriteLine($"Total asset count: {count}");

        handler.Close();
    }
}