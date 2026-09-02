using System;
using System.Collections.Generic;
using System.Linq;
using HoArchive;
using SB09WiiAsset;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel;
using Asset;
using System.Data.Common;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;


namespace CSHO
{
    public static class TargetFetcher
    {
        public static IEnumerable<object> GetTargetInstances(object obj, params Type[] targetTypes)
        {
            if (obj == null || targetTypes == null || targetTypes.Length == 0)
                yield break;

            var type = obj.GetType();

            var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                            .Where(m => m.MemberType == MemberTypes.Field ||
                                        m.MemberType == MemberTypes.Property);

            foreach (var member in members)
            {
                object value = null;

                switch (member)
                {
                    case FieldInfo f:
                        if (targetTypes.Contains(f.FieldType))
                            value = f.GetValue(obj);
                        break;

                    case PropertyInfo p:
                        if (targetTypes.Contains(p.PropertyType) && p.CanRead)
                            value = p.GetValue(obj);
                        break;
                }

                if (value != null)
                    yield return value;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Example.RandomizeSimpleObjects.Run(@"d:\Extracted roms\ToS WII\DATAmod\files\SB09\Levels\SHUB.ho");

            /*string basepath = @"C:\Users\felix\Desktop\Random Stuff\Roms\Wii\ToS - Exctracted\DATA\files\SB09\Levels";
            List<string> paths = new() {"SHUB.ho", "SL01.ho", "SL02.ho", "SL04.ho", "SL05.ho", "SL06.ho", "SL07.ho", "SL08.ho"};

            Handler handler = new();

            double total = 0;
            foreach(string path in paths){
                Stopwatch stopwatch = Stopwatch.StartNew();

                handler.Open(basepath + @"\" + path);
                stopwatch.Stop();

                total += stopwatch.Elapsed.TotalMilliseconds;

                Console.WriteLine($"{stopwatch.Elapsed.TotalMilliseconds:F3} ms");
                handler.Close();
            }

            Console.WriteLine($"Total: {total} ms");
*/
            /*
            // Go through each and every event you can find, log them, and then see which events are in every file 
            List<string> paths = new() {
                @"D:\Extracted roms\ToS WII\DATAOriginal\files\SB09\Levels\SHUB.ho",
                @"D:\Extracted roms\ToS WII\DATAOriginal\files\SB09\Levels\SL05.ho",
                @"D:\Extracted roms\ToS WII\DATAOriginal\files\SB09\Levels\SL06.ho",
                @"D:\Extracted roms\ToS WII\DATAOriginal\files\SB09\Levels\SL07.ho",
                @"D:\Extracted roms\ToS WII\DATAOriginal\files\SB09\Levels\SL08.ho",
                

            };
            Handler handler = new();

            
            List<List<LinkAssetBaseNew>> events = new();
            //List<LinkAsset> linkAssets = new();
            foreach (string path in paths)
            {
                handler.Open(path);
                events.Add(new());

                foreach (TOCEntry entry in handler.GetAssets(enParcelType.PARCEL_TYPE_EXCLUSIVE))
                {
                    //linkAssets.AddRange((LinkAsset[]));
                    foreach (LinkAsset linkAsset in TargetFetcher.GetTargetInstances(entry.entity, typeof(LinkAsset)))
                    {
                        foreach (LinkAssetBaseNew evt in linkAsset.EventLinksArray._linkAssetsBaseNew)
                        {
                            events.Last().Add(evt);
                        }
                    }
                }
                
            }


            List<LinkAssetBaseNew> used = new();
            foreach (LinkAssetBaseNew evt in events[0])
            {
                bool containedinlist = true;
                foreach (List<LinkAssetBaseNew> eventscompare in events.Skip(1))
                {
                    bool iscontained = false;
                    foreach (LinkAssetBaseNew evtcompare in eventscompare)
                    {
                        if (evtcompare.dstAssetID == evt.dstAssetID && (evtcompare.dstEvent.type == evt.dstEvent.type))
                        {
                            iscontained = true;
                            break;
                        }
                    }

                    if (!iscontained)
                    {
                        containedinlist = false;
                        break;
                    }
                }

                foreach (LinkAssetBaseNew evtcompare in used)
                {
                    if (evtcompare.dstAssetID == evt.dstAssetID && (evtcompare.dstEvent.type == evt.dstEvent.type))
                    {
                        containedinlist = false;
                        break;
                    }
                }

                if (containedinlist)
                {
                    Console.WriteLine(evt.dstAssetID.ToString("X16") + " <- " + evt.dstEvent.type.ToString());


                    used.Add(evt);
                }
            }

            /*
            string path = @"C:\Users\felix\Desktop\Random_stuff\XBOX 360\SHUB.ho";
            Handler handler = new();

            handler.Open(path);

            MemoryStreamEndian stream;
            ulong assetid;
            List<Fuck> fucks = new();
            foreach(TOCEntry entry in handler.GetAssets()){
                if(entry.wmlTypeID != wmlTypeID.Texture){continue;}
                stream = new(entry.data.ToArray(), false);
                assetid = stream.ReadUInt64E();
                stream.Dispose();

                fucks.Add(new Fuck((uint)handler.GetAsset(assetid).data.Count, entry.data));
            }

            List<uint> datalengths = new List<uint>();
            foreach(Fuck fuck in fucks){
                if(datalengths.Contains(fuck.size)){continue;}
                datalengths.Add(fuck.size);
            }
            List<byte> matchingdata;
            bool[] mask;
            int count;
            foreach(uint datalength in datalengths){
                matchingdata = new(fucks[0].data);
                mask = new bool[matchingdata.Count];
                for(int b=0; b<mask.Length; b++){mask[b] = true;}
                count = 0;

                foreach(Fuck fuck in fucks){
                    if(fuck.size != datalength){continue;}
                    stream = new(fuck.data.ToArray(), false);
                    stream.Position = 0x0C;
                    if(stream.ReadFloat32E() > 0.5f){ continue; } // Ignore DXT5 Textures
                    stream.Dispose();
                    count++;

                    for(int b=0; b<fuck.data.Count; b++){
                        if(fuck.data[b] != matchingdata[b]){mask[b] = false;}
                    }
                }
                if (count == 0){continue;}
                Console.WriteLine(datalength.ToString() + ": " + count.ToString());
                for(int b=0; b<mask.Length; b+=16){
                    for(int a=0; a<Math.Min(16, mask.Length - b); a++){
                        Console.Write(mask[a+b] ? "1 " : "0 ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            handler.Close();

            string path1 = @"S:\Extracted roms\ToS WII\DATAmod\files\SB09\Levels\Actually SHUB dont mod this one.ho";
            string path2 = @"S:\Extracted roms\TOS WII ESPANOL\DATA\files\sb09\Levels\SHUB - Kopie.ho";

            Handler handler1 = new();
            Handler handler2 = new();

            handler1.Open(path1);
            handler2.Open(path2);

            TOCEntry found;
            foreach(TOCEntry asset in handler1.GetAssets()){ // Loops through all the NTSC SHUB Assets
                if(asset.wmlTypeID == wmlTypeID.Effect){continue;}

                found = handler2.GetAsset(asset.uidSelf);

                if(found == null){continue;}//Console.WriteLine(asset.wmlTypeID.ToString() + ", " + asset.uidSelf.ToString("X16") + " not found in Spanish Version!"); continue;} // NTSC Asset not in Spanish
            
                if(asset.data.Count != found.data.Count){
                    Console.WriteLine(asset.wmlTypeID.ToString() + ", " + asset.uidSelf.ToString("X16") + ": Data Length mismatch!");
                }
                if(!asset.data.SequenceEqual(found.data)){
                    //Console.WriteLine(asset.wmlTypeID.ToString() + ", " + asset.uidSelf.ToString("X16") + ": Data mismatch!");
                    // Find what is mismatching. Is it an AssetID? If yes, Does the target Asset exist in the same file? If yes, we can ignore it. If no, we'll have to investigate.

                }
            }

            handler1.Close();
            handler2.Close();






            //string[] global_files = new string[] {"MNUS.ho", "BULK.ho", @"players\PLYS.ho", @"players\PLYP.ho"};
            //string global_scenes = @"S:\Extracted roms\TOS WII ESPANOL\DATA\files\sb09\global_scenes\";
            
            string path = @"S:\Extracted roms\ToS WII\DATA_NTSC_SL03\files\SB09\Levels\SL09.ho";

            CSHO.Handler handler = new();

            // Copy over any (GenericShaders, Effects) from other parcels and copy them over to the first one
            handler.Open(path);

            Parcel parcel;
            Parcel firstparcel = (Parcel)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[0];
            ParcelTOC toc;
            foreach(ParcelBase parcelbase in handler.GetParcels().Skip(3)){ // Fuck the first three parcels (They useless mofos) 
                if(!(parcelbase is Parcel)){continue;}
                parcel = (Parcel)parcelbase;

                for(int t=0; t<parcel.ParcelTOCs.Count; t++){
                    toc = parcel.ParcelTOCs[t];
                    foreach(TOCEntry asset in toc.Entries){
                        if(asset.wmlTypeID == wmlTypeID.GenericShader){
                            firstparcel.ParcelTOCs[1].Entries.Add(asset.Copy());
                        }
                        else if(asset.wmlTypeID == wmlTypeID.Effect){
                            firstparcel.ParcelTOCs[0].Entries.Add(asset.Copy());
                        }
                        else if(asset.wmlTypeID == wmlTypeID.Material){
                            firstparcel.ParcelTOCs[1].Entries.Add(asset.Copy());
                        }


                        else if(asset.wmlTypeID == wmlTypeID.Model){
                            firstparcel.ParcelTOCs[1].Entries.Add(asset.Copy());
                        }
                        else if(asset.wmlTypeID == wmlTypeID.StaticGeometry){
                            firstparcel.ParcelTOCs[1].Entries.Add(asset.Copy());
                        }
                        else if(asset.wmlTypeID == wmlTypeID.RawBlob){
                            firstparcel.ParcelTOCs[t].Entries.Add(asset.Copy());
                        }

                        else if(asset.wmlTypeID == wmlTypeID.Texture){
                            firstparcel.ParcelTOCs[0].Entries.Add(asset.Copy());
                        }
                        else if(asset.wmlTypeID == wmlTypeID.CollisionMesh){
                            firstparcel.ParcelTOCs[0].Entries.Add(asset.Copy());
                        }
                        else if(asset.wmlTypeID == wmlTypeID.ImmediateGeometry){
                            firstparcel.ParcelTOCs[0].Entries.Add(asset.Copy());
                        }
                        else if(asset.wmlTypeID == wmlTypeID.ImmediateModel){
                            firstparcel.ParcelTOCs[0].Entries.Add(asset.Copy());
                        }
                        
                    }
                }
            }



            handler.path = @"S:\Extracted roms\ToS WII\DATA_RestoredMod\files\SB09\Levels\SL09.ho";
            handler.Archive.MasterTable.StringTable.DomainString = "/sb09/levels/sl09";
            ((Table)handler.Archive.MasterTable.Parcels[0]).StringTable.DomainString = "/sb09/levels/sl09";
            handler.Save();
            handler.Close();
            Console.WriteLine("Done!");
            
            /*

            Handler handler = new();
            handler.Open(@"S:\Extracted roms\ToS WII\DATA_RestoredMod\files\SB09\Levels\SHUB.ho");

            List<byte> datasnippet = new() {0x27, 0xA2, 0x08, 0x03, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x8F, 0x00, 0x01, 0x24, 0x1E};
            List<bool> mask = new() {true, true, true, true, false, false, false, false, true, true, true, true, true, true, true, true};

            List<HoArchive.TOCEntry> assets = handler.GetAssets();
            foreach (HoArchive.TOCEntry entry in assets){
                if(handler.PartOfListMask(entry.data, datasnippet, mask)){
                    
                    Console.WriteLine(entry.uidSelf.ToString("X16"));
                }
            }
            

            /* // Code to strip out every asset that uses global assets
            // Collect all global asset ids
            List<ulong> globalids = new List<ulong>();
            foreach(string global in global_files){
                handler.Open(global_scenes + global);

                foreach(TOCEntry entry in handler.GetAssets()){
                    globalids.Add(entry.uidSelf);
                }

                handler.Close();
            }
            Console.WriteLine("Collected " + globalids.Count.ToString() + " IDs");

            handler.Open(path);
            byte[] id_data;

            int counter;
            foreach(ulong id in globalids){
                id_data = new byte[] {(byte)(id >> 8*7), (byte)(id >> 8*6), (byte)(id >> 8*5), (byte)(id >> 8*4), (byte)(id >> 8*3), (byte)(id >> 8*2), (byte)(id >> 8), (byte)(id)};
                
                counter = 0;
                foreach(TOCEntry found in handler.GetAssetsFromDataSnippet(id_data.ToList())){
                    found.delete = true;
                    counter += 1;
                }

                Console.WriteLine("Iter: 0, Purged: " + counter.ToString() + ", " + id.ToString("X16"));
            }
            int i = 0;
            List<ulong> deletedids;
            while(true){
                deletedids = new List<ulong>();
                foreach(TOCEntry asset in handler.GetAssets()){
                    if(asset.delete){deletedids.Add(asset.uidSelf);}
                }

                if(deletedids.Count == 0){break;} // No more assets marked for deletion! We can stop here.

                handler.Update(); // Delete all the unnecessary assets
                
                foreach(ulong id in deletedids){ // mark assets that referenced the now deleted assets for deletion
                    id_data = new byte[] {(byte)(id >> 8*7), (byte)(id >> 8*6), (byte)(id >> 8*5), (byte)(id >> 8*4), (byte)(id >> 8*3), (byte)(id >> 8*2), (byte)(id >> 8), (byte)(id)};
                    
                    counter = 0;
                    foreach(TOCEntry found in handler.GetAssetsFromDataSnippet(id_data.ToList())){
                        found.delete = true;
                        counter += 1;
                    }

                    Console.WriteLine("Iter: " + i.ToString() + ", Purged: " + counter.ToString() + ", " + id.ToString("X16")); 
                }
                i++;
            }
            handler.path = @"S:\Extracted roms\ToS WII\DATA_NTSC_SL03\files\SB09\Levels\SHUB.ho";
            handler.Archive.MasterTable.StringTable.DomainString = "/sb09/levels/shub";
            ((Table)handler.Archive.MasterTable.Parcels[0]).StringTable.DomainString = "/sb09/levels/shub";
            handler.Save();
            handler.Close();
            Console.WriteLine("Done!");

            string path = "S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SHUB.ho";
            ulong assetid = 0x0000008F000124CA;//0x0000008F000124BF;//

            CSHO.Handler handler = new CSHO.Handler();
            handler.Open(path);
            
            TOCEntry entry = handler.GetAsset(assetid);
            MemoryStreamEndian stream = new MemoryStreamEndian(entry.data.ToArray(), false);
            
            stream.ReadBytes(0x10);
            uint count = stream.ReadUInt32E();
            uint pointer = stream.ReadUInt32E();
            stream.Position = pointer;

            ulong target;
            float time;
            Console.WriteLine("\n\n// Script Info: " + handler.GetName(assetid) + " //\n");

            for(int e = 0; e<count; e++){
                time = stream.ReadFloat32E();
                stream.ReadBytes(0x04);
                target = stream.ReadUInt64E();
                Console.WriteLine(((SB09WiiEvent)stream.ReadUInt32E()).ToString() + " => " + handler.GetName(target) + " [" + target.ToString("X16") + "], T: " + time.ToString());
                stream.ReadBytes(0x0C);
            }
            Console.WriteLine("\n");*/

            /*string contents = File.ReadAllText(@"C:\Users\felix\Desktop\Projects\EventResearch\events.txt");
            List<string> lines = contents.Split("\n").ToList();
            List<uint> eventhashes = new();

            foreach(string line in lines){
                eventhashes.Add(uint.Parse(line.Split(": ")[0], System.Globalization.NumberStyles.HexNumber));
            }
            *//*
            
            string path = "S:\\Extracted roms\\ToS WII\\DATA\\files\\SB09\\Levels\\";
            string[] files = new string[] {"SHUB.ho", "SL01.ho", "SL02.ho", "SL04.ho", "SL05.ho", "SL06.ho", "SL07.ho", "SL08.ho", "SBB1.ho", "SBB2.ho", "SBB3.ho"};

            CSHO.Handler handler = new CSHO.Handler();

            List<TOCEntry> assets;
            foreach (string file in files){
                Console.WriteLine("Searching " + file + "...");
                handler.Open(path + file);
                assets = handler.GetAssets();

                foreach(TOCEntry entry in assets){
                    
                    if (entry.wmlTypeID == wmlTypeID.CurveCamera){ Console.WriteLine(handler.GetName(entry.uidSelf) + ", " + entry.uidSelf.ToString("X16"));}
                }
                

                handler.Close();
            }*/


            //handler.Open("S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\Actually SHUB dont mod this one.ho");


            /*List<TOCEntry> assets = handler.GetAssets();
            StaticGeometry geometry;
            foreach(TOCEntry asset in assets){
                if (asset.wmlTypeID != wmlTypeID.StaticGeometry){continue;}

                geometry = (StaticGeometry)asset.entity;

                uint count = geometry.batchCount;
                if(count != 0)Console.WriteLine(count.ToString() + ", " + asset.uidSelf.ToString("X"));
            }*/

            //handler.path = "S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SHUB.ho";
            //handler.Save();

            //handler.Close();

            /*List<TOCEntry> assets = handler.GetAssets();

            List<byte> eventdata;
            uint count;
            uint srcEvent;
            uint targetEvent;
            foreach(TOCEntry asset in assets){
                if(asset.wmlTypeID == wmlTypeID.SimpleObject){
                    count = ((SimpleObject)asset.entity).EventLinksNew.EventLinksArray.count;
                    eventdata = ((SimpleObject)asset.entity).EventLinksNew.EventLinksArray.data.data;
                }
                else if(asset.wmlTypeID == wmlTypeID.Tiki){
                    count = ((Tiki)asset.entity).EventLinksNew.EventLinksArray.count;
                    eventdata = ((Tiki)asset.entity).EventLinksNew.EventLinksArray.data.data;
                }
                else if(asset.wmlTypeID == wmlTypeID.Counter){
                    count = ((Counter)asset.entity).EventLinksNew.EventLinksArray.count;
                    eventdata = ((Counter)asset.entity).EventLinksNew.EventLinksArray.data.data;
                }
                else if(asset.wmlTypeID == wmlTypeID.Timer){
                    count = ((Timer)asset.entity).EventLinksNew.EventLinksArray.count;
                    eventdata = ((Timer)asset.entity).EventLinksNew.EventLinksArray.data.data;
                }
                else if(asset.wmlTypeID == wmlTypeID.BSP){
                    count = ((BSP)asset.entity).EventLinksNew.EventLinksArray.count;
                    eventdata = ((BSP)asset.entity).EventLinksNew.EventLinksArray.data.data;
                }
                else{continue;}

                //Console.WriteLine(eventdata.Count);
                for(int e=0; e<count; e++){
                    srcEvent = ((uint)eventdata[e*0x28] << 24) + ((uint)eventdata[e*0x28+1] << 16) + ((uint)eventdata[e*0x28+2] << 8) + ((uint)eventdata[e*0x28+3]);
                    targetEvent = ((uint)eventdata[e*0x28+8] << 24) + ((uint)eventdata[e*0x28+9] << 16) + ((uint)eventdata[e*0x28+10] << 8) + ((uint)eventdata[e*0x28+11]);

                    if(!eventhashes.Contains(srcEvent)){Console.WriteLine("Undocumented Source Event found! Hash: " + srcEvent.ToString("X") + ", AssetID: " + asset.uidSelf.ToString("X"));}
                    if(!eventhashes.Contains(targetEvent)){Console.WriteLine("Undocumented Target Event found! Hash: " + targetEvent.ToString("X") + ", AssetID: " + asset.uidSelf.ToString("X"));}
                }
                //if(simp.modelInstance.instanceParamCount != 0){Console.WriteLine(simp.id);}
            }

            */
            /*CSHO.Handler ArchiveHandler = new CSHO.Handler(); // Initialising your Handler
            
            string errorcode;
            //errorcode = ArchiveHandler.("S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SHUB - 1x1x1.ho");
            //errorcode = ArchiveHandler.NewFrom("S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SHUB Original.ho"); // Open an archive
            errorcode = ArchiveHandler.NewFromLSET("C:\\Users\\felix\\Desktop\\Projects\\TOSSlideMod\\Slide\\SHUB\\SHUB.lset");//"S:\\Extracted roms\\TOS WII ESPANOL\\DATA\\files\\sb09\\Levels\\SBB3.ho"); // Open an archive

            //Error Handeling
            if (errorcode != ""){ // Handler.Open() returns a string as an errorcode. It will return "" if the operation succeeded.
                Console.WriteLine("Exception: " + errorcode);
                Environment.Exit(1);
            }

            Console.WriteLine(ArchiveHandler.Archive.Header.target);
            //ArchiveHandler.ExportLSET("C:\\Users\\felix\\Desktop\\Projects\\TOSSlideMod\\Slide");

            //Console.WriteLine(ArchiveHandler.Archive.MasterTable.TableEntries[0].parcelType.ToString());

            //ArchiveHandler.NewAsset(parcel.ParcelTOCs[0], 0x8008800880088008, 0x55555555, "C:\\Users\\felix\\Desktop\\test.dat"); // Here we pass it the first asset table, an assetid, a type and a path to a file we want to store.

            //HoArchive.ParcelDebug debugparcel = (HoArchive.ParcelDebug)secttable.Parcels[1]; // Here we select the second Parcel ("PD  "), which is a Debug Parcel. It contains asset names.
            //ArchiveHandler.NewNameTableEntry(debugparcel, 0x8008800880088008, "Test Asset!"); // Then we add a new name table entry to add the assets name.
            // This casting is needed because because both parceltypes (Parcel and ParcelDebug) share the same list, thus the compiler needs to know what parceltype is being worked on right now.

            //SB09WiiAsset.SimpleObject testsimpleobject = ((SB09WiiAsset.SimpleObject)ArchiveHandler.GetAsset(528564445308792).entity);
            //testsimpleobject.Pos = new HoArchive.float3(0, 0, 0);
            Console.WriteLine("Packed successfully!");

            ArchiveHandler.path = "S:\\Extracted roms\\ToS WII\\DATAmod\\files\\SB09\\Levels\\SHUB.ho";
            ArchiveHandler.Save();*/

            /*
            List<TOCEntry> p0 = new List<TOCEntry>();
                        List<TOCEntry> p1 = new List<TOCEntry>();
                        List<TOCEntry> p2 = new List<TOCEntry>();
                        List<TOCEntry> ptex = new List<TOCEntry>();
                        List<TOCEntry> pfst = new List<TOCEntry>();
                        List<NameTableEntry> debugparcels = new List<NameTableEntry>();

                        ParcelBase parcel;
                        TableEntry entry;
                        foreach(string global in global_files){
                            handler.Open(global_scenes + global);
                            Console.WriteLine(global);


                            for(int i=0; i<((Table)handler.Archive.MasterTable.Parcels[0]).TableEntries.Count; i++){
                                parcel = ((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[i];
                                entry = ((Table)handler.Archive.MasterTable.Parcels[0]).TableEntries[i];

                                if(entry.sectionType == "P   "){
                                    if(((Parcel)parcel).ParcelTOCs.Count > 0){p0.AddRange(((Parcel)parcel).ParcelTOCs[0].Entries);}
                                    if(((Parcel)parcel).ParcelTOCs.Count > 1){p1.AddRange(((Parcel)parcel).ParcelTOCs[1].Entries);}
                                    if(((Parcel)parcel).ParcelTOCs.Count > 2){p2.AddRange(((Parcel)parcel).ParcelTOCs[2].Entries);}
                                }
                                else if(entry.sectionType == "PTEX"){
                                    ptex.AddRange(((Parcel)parcel).ParcelTOCs[0].Entries);
                                }
                                else if(entry.sectionType == "PFST"){
                                    pfst.AddRange(((Parcel)parcel).ParcelTOCs[0].Entries);
                                }
                                else{
                                    debugparcels.AddRange(((ParcelDebug)parcel).NameTableEntries);
                                }
                            }

                            handler.Close();
                        }

                        handler.Open(path);

                        ((Parcel)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[0]).ParcelTOCs[0].Entries.AddRange(p0);
                        ((Parcel)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[0]).ParcelTOCs[1].Entries.AddRange(p1);
                        ((Parcel)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[0]).ParcelTOCs[2].Entries.AddRange(p2);

                        ((Parcel)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[2]).ParcelTOCs[0].Entries.AddRange(ptex);
                        handler.NewParcel((Table)handler.Archive.MasterTable.Parcels[0], "PFST");
                        ((Parcel)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[17]).ParcelTOCs.Add(new ParcelTOC());
                        ((Parcel)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[17]).ParcelTOCs[0].Entries.AddRange(pfst);

                        ((ParcelDebug)((Table)handler.Archive.MasterTable.Parcels[0]).Parcels[1]).NameTableEntries.AddRange(debugparcels);       


                        //*/
        }
    }
}