using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class GameData
{
    public bool isNew = true;
    public string FinalTime;

    #region Story

    public bool Story_00;
    public bool Story_01;

    #endregion

    public int coins;
    public PlayerData playerData;
}
