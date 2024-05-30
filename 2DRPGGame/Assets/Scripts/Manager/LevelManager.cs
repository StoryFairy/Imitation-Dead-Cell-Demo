using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Corgi Engine/Managers/Level Manager")]
public class LevelManager : MMPersistenSingleton<LevelManager>
{
    public string LevelName;
    
    public float IntroFadeDuration = 1f;
    public float OutroFadeDuration = 1f;
    public int FaderID = 0;
    public MMTweenType FadeTween = new MMTweenType(MMTween.MMTweenCurve.EaseInOutCubic);
    
    public string LoadingSceneName = "LoadingScreen";

    /// the elapsed time since the start of the level
    public TimeSpan RunningTime
    {
        get { return DateTime.UtcNow - _started; }
    }

    // private stuff
    protected DateTime _started;
    protected int _savedPoints;
    protected string _nextLevel = null;
    protected BoxCollider _collider;
    protected BoxCollider2D _collider2D;
    protected Bounds _originalBounds;

    protected override void Awake()
    {
        base.Awake();
    }

    public void GetLevelName() => LevelName = SceneManager.GetActiveScene().name;

    protected virtual void InstantiatePlayableCharacters()
    {
        //Debug.Log("LevelManager----InstantiatePlayableCharacters");
    }

    public virtual void Start()
    {
        //Debug.Log("LevelManager------Start");
    }

    protected virtual void Initialization()
    {
        //Debug.Log("LevelManager------Initialization");
    }

    public virtual void GotoLevel(string levelName, bool fadeOut = true, bool save = true)
    {
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.LevelEnd);
        if (save)
        {
            MMGameEvent.Trigger("Save");
        }

        if (fadeOut)
        {
            MMFadeInEvent.Trigger(OutroFadeDuration, FadeTween, FaderID, true, Vector3.zero);
        }

        StartCoroutine(GotoLevelCo(levelName, fadeOut));
    }
    
    protected virtual IEnumerator GotoLevelCo(string levelName, bool fadeOut = true)
    {
        if (fadeOut)
        {
            if (Time.timeScale > 0.0f)
            {
                yield return new WaitForSeconds(OutroFadeDuration);
            }
            else
            {
                yield return new WaitForSecondsRealtime(OutroFadeDuration);
            }
        }
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.UnPause);
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.LoadNextScene);
        string destinationScene = (string.IsNullOrEmpty(levelName)) ? "02-StartScreen" : levelName;
        MMSceneLoadingManager.LoadScene(destinationScene,LoadingSceneName);
    }
}
