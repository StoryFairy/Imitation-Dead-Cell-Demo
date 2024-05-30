using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UnderGroundRoomTemplatesConfig
{
    #region 模版

    public GameObject[] DefaultRoomTemplates;
    public GameObject[] ShopRoomTemplates;
    
    public GameObject[] TreasureRoomTemplates;
    public GameObject[] EntranceRoomTemplates;
    public GameObject[] ExitRoomTemplates;
    
    public GameObject[] CorridorRoomTemplates;

    #endregion

    public GameObject[] GetRoomTemplate(UnderGroundRoom room)
    {
        switch (room.Type)
        {
            case UnderGroundRoomType.Shop:
                return ShopRoomTemplates;
            case UnderGroundRoomType.Treasure:
                return TreasureRoomTemplates;
            case UnderGroundRoomType.Entrance:
                return EntranceRoomTemplates;
            case UnderGroundRoomType.Exit:
                return ExitRoomTemplates;
            default:
                return DefaultRoomTemplates;
        }
    }
}
