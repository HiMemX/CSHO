using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset{
    public class TriggerOG : xBaseAsset{
        [TypeConverter(typeof(enTriggerSubtypeConverter))]
        public enTriggerSubtype subtype {get;set;}
        public bool Targettable {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get;set;}

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TriggerSubtype triggerSubtype {get;set;}

        // Different from dwarf
        [TypeConverter(typeof(Point3Converter))]
        public float3 Dir {get;set;}

        public int DirFlag {get;set;}

        public TriggerOG(HoArchive.MemoryStreamEndian file) : base(file){
            subtype = (enTriggerSubtype)file.ReadByte();
            Targettable = file.ReadBool();
            file.Align(0x04);
            EventLinksNew = new LinkAsset(file);
            
            file.Position = 0x20;
            switch(subtype){
                case enTriggerSubtype.BOX:
                    triggerSubtype = new TriggerBox(file);
                    break;

                case enTriggerSubtype.SPHERE:
                    triggerSubtype = new TriggerSphere(file);
                    break;
                
                case enTriggerSubtype.CYLINDER:
                    triggerSubtype = new TriggerCylinder(file);
                    break;
                    
                case enTriggerSubtype.CIRCLE:
                    triggerSubtype = new TriggerCircle(file);
                    break;
                    
                case enTriggerSubtype.POLYGON:
                    triggerSubtype = new TriggerPolygon(file);
                    break;
            }



            file.Position = 0x50;
            Dir = new float3(file);
            DirFlag = file.ReadInt32E();
        }

        public override void Update(HoArchive.TOCEntry entry){
            base.Update(entry);
            
            switch(subtype){
                case enTriggerSubtype.BOX:
                    if (triggerSubtype is not TriggerBox) triggerSubtype = new TriggerBox();
                    break;

                case enTriggerSubtype.SPHERE:
                    if (triggerSubtype is not TriggerSphere) triggerSubtype = new TriggerSphere();
                    break;
                
                case enTriggerSubtype.CYLINDER:
                    if (triggerSubtype is not TriggerCylinder) triggerSubtype = new TriggerCylinder();
                    break;
                    
                case enTriggerSubtype.CIRCLE:
                    if (triggerSubtype is not TriggerCircle) triggerSubtype = new TriggerCircle();
                    break;
                    
                case enTriggerSubtype.POLYGON:
                    if (triggerSubtype is not TriggerPolygon) triggerSubtype = new TriggerPolygon();
                    break;
            }
            EventLinksNew.Update();
            
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);

            file.WriteE((byte)subtype);
            file.WriteE(Targettable);
            file.Pad(2, 0);

            EventLinksNew.Save(file);

            file.PadTo(0x20, 0);
            triggerSubtype.Save(file);
            
            file.PadTo(0x50, 0);
            Dir.Save(file);
            file.WriteE(DirFlag);

            EventLinksNew.SaveHeap(file);
        }
    }
}