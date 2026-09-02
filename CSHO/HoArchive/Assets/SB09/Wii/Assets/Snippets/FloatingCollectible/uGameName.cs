using System;
using System.ComponentModel;
using HoArchive;

namespace SB09WiiAsset;

public class uGameName{
    [TypeConverter(typeof(eSBFloatingItemTypeConverter))]
    public eUPFloatingItemType UPItemType{get;set;}
    [TypeConverter(typeof(eSBFloatingItemTypeConverter))]
    public eSBFloatingItemType SBItemType {get;set;}

    public uGameName(MemoryStreamEndian file, GameName game){
        if(game == GameName.UP09){
            UPItemType = (eUPFloatingItemType)file.ReadUInt32E();
            //throw new System.NotImplementedException();
        }
        if(game == GameName.SB09){
            SBItemType = (eSBFloatingItemType)file.ReadUInt32E();
        }
    }

    public void Save(MemoryStreamEndian file, GameName game){
        if(game == GameName.UP09){
            file.WriteE((uint)UPItemType);
        }
        if(game == GameName.SB09){
            file.WriteE((uint)SBItemType);
        }
    }
}