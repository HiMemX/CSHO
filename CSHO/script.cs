/*string path = @"S:\Extracted roms\ToS WII\DATA_NTSC_SL03\files\SB09\Levels\SL09.ho";

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
Console.WriteLine("Done!");*/