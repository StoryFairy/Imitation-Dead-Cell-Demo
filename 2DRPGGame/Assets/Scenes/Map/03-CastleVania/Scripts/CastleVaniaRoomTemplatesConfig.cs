using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CastleVaniaRoomTemplatesConfig
{
    #region 模版

    public GameObject[] EntranceRoomTemplates;
    public GameObject[] ExitRoomTemplates;
    public GameObject[] CorridorRoomTemplates;

    #endregion

    public GameObject[] GetRoomTemplate(CastleVaniaRoom room)
    {
        switch (room.Type)
        {
            case CastleVaniaRoomType.Entrance:
                return EntranceRoomTemplates;
            case CastleVaniaRoomType.Exit:
                return ExitRoomTemplates;
        }

        return null;
    }
}
