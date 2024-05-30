using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CorgiEngineEventTypes
{
    SpawnCharacterStarts,//角色生成
    LevelStart,//关卡开始
    LevelComplete,//关卡完成
    LevelEnd,//关卡结束
    Pause,//暂停
    UnPause,//取消暂停
    PlayerDeath,//玩家死亡
    Respawn,//重生
    StarPicked,//摘星*
    GameOver,//游戏结束
    CharacterSwitch,//角色切换
    CharacterSwap,//角色交换
    TogglePause,//暂停切换
    LoadNextScene//加载下一个场景
}

public struct CorgiEngineEvent
{
    public CorgiEngineEventTypes EventType;
    
    public CorgiEngineEvent(CorgiEngineEventTypes eventType)
    {
        EventType = eventType;
    }

    static CorgiEngineEvent e;

    public static void Trigger(CorgiEngineEventTypes eventType)
    {
        e.EventType = eventType;
        MMEventManager.TriggerEvent(e);
    }
}

public enum PauseMethods//是否需要打开暂停菜单
{
    PauseMenu,
    NoPauseMenu
}

[AddComponentMenu("Corgi Engine/Manager/Game Manager")]
public class GameManager : MMPersistenSingleton<GameManager>, IMMEventListener<MMGameEvent>,
    IMMEventListener<CorgiEngineEvent>
{
    public GameDataSO gameDataSO;
    
    /// true if the game is currently paused
    public bool Paused { get; set; }

    // true if we've stored a map position at least once
    public bool StoredLevelMapPosition { get; set; }

    /// the current player
    public Vector2 LevelMapPosition { get; set; }
    
    protected bool _pauseMenuOpen = false;
    protected int _initialMaximumLives;
    protected int _initialCurrentLives;


    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// On Start(), sets the target framerate to whatever's been specified
    /// </summary>
    protected virtual void Start()
    {
        
    }

    /// <summary>
    /// this method resets the whole game manager
    /// </summary>
    public virtual void Reset()
    {
        Debug.Log("GameManager-----Reset");
    }
    
    /// <summary>
    /// Pauses the game or unpauses it depending on the current state
    /// </summary>
    public virtual void Pause(PauseMethods pauseMethod = PauseMethods.PauseMenu)
    {
        if (Time.timeScale > 0.0f)
        {
            Time.timeScale = 0f;
            Instance.Paused = true;
            if ((GUIManager.HasInstance) && (pauseMethod == PauseMethods.PauseMenu))
            {
                GUIManager.Instance.SetPause(true);
                
                _pauseMenuOpen = true;
            }
            if (pauseMethod == PauseMethods.NoPauseMenu)
            {
                _pauseMenuOpen = false;
            } 
        }
    }

    /// <summary>
    /// Unpauses the game
    /// </summary>
    public virtual void UnPause(PauseMethods pauseMethod = PauseMethods.PauseMenu)
    {
        Time.timeScale = 1f;
        Instance.Paused = false;
        if ((GUIManager.HasInstance) && (pauseMethod == PauseMethods.PauseMenu))
        {
            GUIManager.Instance.SetPause(false);
            _pauseMenuOpen = false;
        }
    }
    
    /// <summary>
    /// Catches MMGameEvents and acts on them, playing the corresponding sounds
    /// </summary>
    /// <param name="gameEvent">MMGameEvent event.</param>
    public void OnMMEvent(MMGameEvent gameEvent)
    {
        if (gameEvent.EventName == "Save")
        {
            if(gameDataSO!=null)
                gameDataSO.SaveGameData();
        }
    }

    /// <summary>
    /// Catches CorgiEngineEvents and acts on them, playing the corresponding sounds
    /// </summary>
    /// <param name="engineEvent">CorgiEngineEvent event.</param>
    public void OnMMEvent(CorgiEngineEvent engineEvent)
    {
        switch (engineEvent.EventType)
        {
            case CorgiEngineEventTypes.TogglePause:
                if (Paused)
                {
                    CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
                }
                else
                {
                    CorgiEngineEvent.Trigger(CorgiEngineEventTypes.Pause);
                }
                break;

            case CorgiEngineEventTypes.Pause:
                Pause();
                break;

            case CorgiEngineEventTypes.UnPause:
                UnPause();
                break;
        }
    }

    protected virtual void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
        this.MMEventStartListening<CorgiEngineEvent>();
    }

    protected virtual void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
        this.MMEventStopListening<CorgiEngineEvent>();
    }

    #region Other函数

    public float RandomNumber()
    {
        return Random.Range(0, 100) + 1;
    }

    #endregion
}


