using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public string NextLevel;
    public float AutoSkipDelay = 0f;
    public bool SkipLoading = true;
    
    public float FadeInDuration = 1f;
    public float FadeOutDuration = 1f;
    public int FadeId = 0;
    public bool UseFadein = true;
    public MMTweenType Tween;

    protected virtual void Awake()
    {
        if (AutoSkipDelay > 0f)
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    protected virtual IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(AutoSkipDelay);
        if (UseFadein)
        {
            MMFadeInEvent.Trigger(FadeInDuration, Tween, FadeId, true);
            yield return new WaitForSeconds(FadeInDuration + 0.5f);
        }
        if (SkipLoading)
        {
            SceneManager.LoadScene(NextLevel);
        }
        else
        {
            MMSceneLoadingManager.LoadScene(NextLevel);
        }
    }
}
