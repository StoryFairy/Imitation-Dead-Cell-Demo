using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ForestRoomTemplatesConfig
{
    #region 模版

    public GameObject[] DefaultRoomTemplates;
    public GameObject[] ShopRoomTemplates;
    
    public GameObject[] TreasureRoomTemplates;
    public GameObject[] EntranceRoomTemplates;
    public GameObject[] ExitRoomTemplates;
    
    public GameObject[] CorridorRoomTemplates;

    #endregion

    public GameObject[] GetRoomTemplate(ForestRoom room)
    {
        switch (room.Type)
        {
            case ForestRoomType.Shop:
                return ShopRoomTemplates;
            case ForestRoomType.Treasure:
                return TreasureRoomTemplates;
            case ForestRoomType.Entrance:
                return EntranceRoomTemplates;
            case ForestRoomType.Exit:
                return ExitRoomTemplates;
            default:
                return DefaultRoomTemplates;
        }
    }
}
