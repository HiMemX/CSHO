using System.ComponentModel;

namespace SB09WiiAsset{

    public class UVMovementSettings : Asset.AssetEntity{
        public float InitialDegree {get; set;}
        public float DegreePerSec {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UVSet InitialScale {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UVSet ScalePerSec {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UVSet InitialTranslate {get; set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public UVSet TranslatePerSec {get; set;}

        public UVMovementSettings(HoArchive.MemoryStreamEndian file){
            InitialDegree = file.ReadFloat32E();
            DegreePerSec = file.ReadFloat32E();
            InitialScale = new UVSet(file);
            ScalePerSec = new UVSet(file);
            InitialTranslate = new UVSet(file);
            TranslatePerSec = new UVSet(file);
        }

        public override void Update(HoArchive.TOCEntry entry){
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            file.WriteE(InitialDegree);
            file.WriteE(DegreePerSec);
            InitialScale.Save(file);
            ScalePerSec.Save(file);
            InitialTranslate.Save(file);
            TranslatePerSec.Save(file);
        }
    }
}