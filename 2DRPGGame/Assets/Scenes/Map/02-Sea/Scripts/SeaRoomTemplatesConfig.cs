using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SeaRoomTemplatesConfig
{
    #region 模版

    public GameObject[] DefaultRoomTemplates;
    public GameObject[] ShopRoomTemplates;
    
    public GameObject[] TreasureRoomTemplates;
    public GameObject[] EntranceRoomTemplates;
    public GameObject[] ExitRoomTemplates;
    
    public GameObject[] CorridorRoomTemplates;

    #endregion

    public GameObject[] GetRoomTemplate(SeaRoom room)
    {
        switch (room.Type)
        {
            case SeaRoomType.Shop:
                return ShopRoomTemplates;
            case SeaRoomType.Treasure:
                return TreasureRoomTemplates;
            case SeaRoomType.Entrance:
                return EntranceRoomTemplates;
            case SeaRoomType.Exit:
                return ExitRoomTemplates;
            default:
                return DefaultRoomTemplates;
        }
    }
}
