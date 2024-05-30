using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "MoreMountains/GameData/GameDataSO")]
public class GameDataSO : ScriptableObject
{
    public string _saveFolderName = "MMGameData/";
    public string _saveFileName = "mmData.settings";
    
    public GameData GameData;


    public void Initialize()
    {
        GameData.isNew = false;
        GameData.FinalTime = DateTime.Now.ToString("yyyy/MM/dd/\nHH:mm");
        GameData.Story_00 = false;
        GameData.Story_01 = false;

        GameData.coins = 0;
        GameData.playerData = new PlayerData();
    }

    #region SaveAndLoad

    public virtual void SaveGameData()
    {
        if(GameData.isNew)
            Initialize();
        MMSaveLoadManager.Save(this.GameData, _saveFileName, _saveFolderName);
    }

    public virtual void LoadGameData()
    {
        GameData gameData =
            (GameData)MMSaveLoadManager.Load(typeof(GameData), _saveFileName,
                _saveFolderName);
        if (gameData != null)
        {
            this.GameData = gameData;
        }
    }

    public virtual void UninstallGameData()
    {
        MMSaveLoadManager.DeleteSave(_saveFileName, _saveFolderName);
        GameData.isNew = true;
    }

    #endregion
}
