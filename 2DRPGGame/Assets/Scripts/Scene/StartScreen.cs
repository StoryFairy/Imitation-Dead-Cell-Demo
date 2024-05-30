using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public string NextLevel;

    [Header("Fades")] public float FadeInDuration = 1f;
    public float FadeOutDuration = 1f;
    public MMTweenType Tween;

    [Header("Sound Settings Bindings")] public MMSwitch MusicSwitch;
    public MMSwitch SfxSwitch;

    [Header("Archives")] public GameDataSO[] gameDataSOs;
    public GameObject[] ArchiveTexts;
    public GameObject[] Uninstall;

    protected async void Start()
    {
        MMFadeOutEvent.Trigger(FadeInDuration, Tween);
        await Task.Delay(1);
        SoundManagerDataInit();
        GameDataInit();
    }

    #region Save&Load

    public void CreateGameDataSO1()
    {
        MMFadeInEvent.Trigger(FadeOutDuration, Tween, 0, true);
        GameManager.Instance.gameDataSO = gameDataSOs[0];
        StartCoroutine(LoadFirstLevel(0));
    }

    public void DeleteGameDataSO1()
    {
        gameDataSOs[0].UninstallGameData();
        GameDataInit();
    }

    public void CreateGameDataSO2()
    {
        MMFadeInEvent.Trigger(FadeOutDuration, Tween, 0, true);
        GameManager.Instance.gameDataSO = gameDataSOs[1];
        StartCoroutine(LoadFirstLevel(1));
    }

    public void DeleteGameDataSO2()
    {
        gameDataSOs[1].UninstallGameData();
        GameDataInit();
    }
    
    public void LoadGameData(int index)
    {
        
    }

    IEnumerator LoadFirstLevel(int number)
    {
        yield return new WaitForSeconds(FadeOutDuration);
        gameDataSOs[number].SaveGameData();
        MMSceneLoadingManager.LoadScene(NextLevel);
    }
    
    #endregion

    #region Init

        private void GameDataInit()
        {
            int index = 0;
            foreach (var gameDataSO in gameDataSOs)
            {
                var text = ArchiveTexts[index].GetComponent<Text>();
                if (!gameDataSO.GameData.isNew)
                {
                    gameDataSO.LoadGameData();
                    if (gameDataSO.GameData != null)
                    {
                        text.text = "激活\n\n上一次游戏：\n" + gameDataSO.GameData.FinalTime;
                        Uninstall[index].SetActive(true);
                    }
                }
                else
                {
                    text.text = "空白";
                    Uninstall[index].SetActive(false);
                }
    
                index++;
            }
        }
    
        private void SoundManagerDataInit() //根据保存记录初始化MusicSwitch和SfxSwitch状态
        {
            if (MusicSwitch != null)
            {
                MusicSwitch.CurrentSwitchState = MMSoundManager.Instance.settingsSo.Settings.MusicOn
                    ? MMSwitch.SwitchStates.Right
                    : MMSwitch.SwitchStates.Left;
                MusicSwitch.InitializeState();
            }
    
            if (SfxSwitch != null)
            {
                SfxSwitch.CurrentSwitchState = MMSoundManager.Instance.settingsSo.Settings.SfxOn
                    ? MMSwitch.SwitchStates.Right
                    : MMSwitch.SwitchStates.Left;
                SfxSwitch.InitializeState();
            }
        }

    #endregion


}
