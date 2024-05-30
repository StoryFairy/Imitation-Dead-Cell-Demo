using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CastleRoomTemplatesConfig
{
    #region 模版

    public GameObject[] MainRoomTemplates;

    #endregion

    public GameObject[] GetRoomTemplate(CastleRoom room)
    {
        return MainRoomTemplates;
    }
}
