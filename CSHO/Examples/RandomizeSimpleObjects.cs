using System;
using System.Collections.Generic;
using CSHO;
using HoArchive;

namespace Example;

public static class RandomizeSimpleObjects
{
    public static void Run(string path)
    {
        /// <summary>
        ///  Randomize all simple object positions of a .ho archive (SB09, Wii)
        /// </summary>
        
        Handler handler = new Handler(); // create archive handler
        handler.Open(path); // Open .ho archive

        List<TOCEntry> assets = handler.GetAssets(); // fetch every single asset in archive

        Random rng = new Random(); // init random number generator

        foreach(TOCEntry asset in assets) // Iterate through all assets
        {
            if (!(asset.entity is SB09WiiAsset.SimpleObject)) continue; // If asset isn't simpleobject (SB09Wii), then skip

            // Jiggle position randomly
            SB09WiiAsset.SimpleObject entity = (SB09WiiAsset.SimpleObject)asset.entity;
            entity.Pos.x += 1 * (rng.NextSingle() - 0.5f);
            entity.Pos.y += 1 * (rng.NextSingle() - 0.5f);
            entity.Pos.z += 1 * (rng.NextSingle() - 0.5f);
        }

        // handler.path = "New Path/archive.ho"; << Uncomment if you want to change path of file
        handler.Save(); // Save archive
        handler.Close();
    }
}