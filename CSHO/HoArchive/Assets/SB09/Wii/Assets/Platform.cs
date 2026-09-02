using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HoArchive;

namespace SB09WiiAsset{
    public class Platform : xEntAsset{
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LinkAsset EventLinksNew {get; set;}

        // Different from Dwarf because of unions
        [TypeConverter(typeof(enPlatformTypeConverter))]
        public enPlatformType type {get;set;}
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PlatformType platformType {get;set;}
        
        public bool CameraCollisionOff {get;set;}
        public bool GoTransparent {get;set;}
        public bool BIPLANABLE {get;set;}
        public bool SBCostumeChange {get;set;}
        public bool ForceUpdateDistance {get;set;}
        public float ForceUpdateDistanceValue {get;set;}

        [TypeConverter(typeof(AssetIDConverter))]
        public ulong Destructible {get;set;}

        public Platform(HoArchive.MemoryStreamEndian file) : base(file){
            EventLinksNew = new LinkAsset(file);
            type = (enPlatformType)file.ReadByte();
            
            switch(type){
                case enPlatformType.SPLINE:
                    platformType = new SplineType(file);
                    break;
                
                case enPlatformType.MOVEPOINT:
                    platformType = new MovepointType(file);
                    break;

                case enPlatformType.MECHANISM:
                    platformType = new MechanismType(file);
                    break;

                case enPlatformType.CONVEYOR_BELT:
                    platformType = new ConveyorBeltType(file);
                    break;

                case enPlatformType.TEETER:
                    platformType = new TeeterType(file);
                    break;

                case enPlatformType.FULLY_MANIPULABLE:
                    platformType = new FullyManipulableType(file);
                    break; 
            }

            file.Align(0x40);

            CameraCollisionOff = file.ReadBool();
            GoTransparent = file.ReadBool();
            BIPLANABLE = file.ReadBool();
            SBCostumeChange = file.ReadBool();
            ForceUpdateDistance = file.ReadBool();
            file.Align(0x04);

            ForceUpdateDistanceValue = file.ReadFloat32E();
            file.Align(0x10);
            Destructible = file.ReadUInt64E();
        }

        public override void Update(HoArchive.TOCEntry entry)
        {
            base.Update(entry);

            if (platformType.type != type)
            {
                switch (type)
                {
                    case enPlatformType.SPLINE:
                        platformType = new SplineType();
                        break;

                    case enPlatformType.MOVEPOINT:
                        platformType = new MovepointType();
                        break;

                    case enPlatformType.MECHANISM:
                        platformType = new MechanismType();
                        break;

                    case enPlatformType.CONVEYOR_BELT:
                        platformType = new ConveyorBeltType();
                        break;

                    case enPlatformType.TEETER:
                        platformType = new TeeterType();
                        break;

                    case enPlatformType.FULLY_MANIPULABLE:
                        platformType = new FullyManipulableType();
                        break;
                }
            }

            EventLinksNew.Update();
        }

        public override void Save(HoArchive.MemoryStreamEndian file){
            base.Save(file);

            EventLinksNew.Save(file);
        
            file.WriteE((byte)type);
            platformType.Save(file);
            file.Align(0x40);

            file.WriteE(CameraCollisionOff);
            file.WriteE(GoTransparent);
            file.WriteE(BIPLANABLE);
            file.WriteE(SBCostumeChange);
            file.WriteE(ForceUpdateDistance);
            file.PadAlign(0x04, 0);
            file.WriteE(ForceUpdateDistanceValue);
            file.PadAlign(0x10, 0);
            file.WriteE(Destructible);
            file.PadAlign(0x10, 0);
            
            base.SaveHeap(file);
            EventLinksNew.SaveHeap(file);
        }
    }
}