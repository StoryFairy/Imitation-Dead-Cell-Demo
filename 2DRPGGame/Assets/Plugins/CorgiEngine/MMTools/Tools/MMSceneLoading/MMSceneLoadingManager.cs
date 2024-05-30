using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMSceneLoadingManager : MonoBehaviour
{
    public enum LoadingStatus
    {
        LoadStarted,//加载已开始
        BeforeEntryFade,//在进入淡出之前
        EntryFade,//进入淡出
        AfterEntryFade,//在进入淡出之后
        UnloadOriginScene,//卸载原点场景
        LoadDestinationScene,//加载目的地场景
        LoadProgressComplete,//加载进度完成
        InterpolatedLoadProgressComplete,//异步加载进度完成
        BeforeExitFade,//在退出淡出之前
        ExitFade,//退出淡出
        DestinationSceneActivation,//目的地场景激活
        UnloadSceneLoader,//卸载场景加载器
        LoadTransitionComplete//负载转换完成
    }

    public struct LoadingSceneEvent
    {
        public LoadingStatus Status;
        public string SceneName;

        public LoadingSceneEvent(string sceneName, LoadingStatus status)
        {
            Debug.Log("MMSceneLoadingManager-----LoadingSceneEvent");
            Status = status;
            SceneName = sceneName;
        }

        static LoadingSceneEvent e;

        public static void Trigger(string sceneName, LoadingStatus status)
        {
            e.Status = status;
            e.SceneName = sceneName;
            MMEventManager.TriggerEvent(e);
        }
    }

    [Header("Binding")] public static string LoadingScreenSceneName = "LoadingScreen";

    [Header("GameObjects")] public Text LoadingText;
    public CanvasGroup LoadingProgressBar;
    public CanvasGroup LoadingAnimation;
    public CanvasGroup LoadingCompleteAnimation;

    [Header("Time")] public float StartFadeDuration = 0.2f;
    public float ProgressBarSpeed = 2f;
    public float ExitFadeDuration = 0.2f;
    public float LoadCompleteDelay = 0.5f;

    protected AsyncOperation _asyncOperation; //控制loadingBar
    protected static string _sceneToLoad = "";
    protected float _fadeDuration = 0.5f;
    protected float _fillTarget = 0f;
    protected string _loadingTextValue;
    protected Image _progressBarImage;

    protected static MMTweenType _tween;


    public static void LoadScene(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
        Application.backgroundLoadingPriority = ThreadPriority.High;

        if (LoadingScreenSceneName != null)
        {
            LoadingSceneEvent.Trigger(sceneToLoad, LoadingStatus.LoadStarted);
            SceneManager.LoadScene(LoadingScreenSceneName);
        }
    }

    public static void LoadScene(string sceneToLoad, string loadingSceneName)
    {
        _sceneToLoad = sceneToLoad;
        Application.backgroundLoadingPriority = ThreadPriority.High;
        LoadingSceneEvent.Trigger(sceneToLoad, LoadingStatus.LoadStarted);
        SceneManager.LoadScene(loadingSceneName);
    }

    protected virtual void Start()
    {
        _tween = new MMTweenType(MMTween.MMTweenCurve.EaseOutCubic);
        _progressBarImage = LoadingProgressBar.GetComponent<Image>();
        _loadingTextValue = LoadingText.text;
        if (!string.IsNullOrEmpty(_sceneToLoad))
        {
            StartCoroutine(LoadAsynchronously());
        }
    }

    protected virtual void Update()
    {
        Time.timeScale = 1f;
        //根据指定的速度从“from”移动到“to”并返回相应的值
        //让进度条很顺滑的进行变化
        _progressBarImage.fillAmount =
            MMMaths.Approach(_progressBarImage.fillAmount, _fillTarget, Time.deltaTime * ProgressBarSpeed);
    }

    protected virtual IEnumerator LoadAsynchronously()
    {
        // 设置了各种视觉元素
        LoadingSetup();
        //广播淡出效果
        MMFadeOutEvent.Trigger(StartFadeDuration, _tween);
        yield return new WaitForSeconds(StartFadeDuration);
        //在后台加载一个场景。在此期间，会加载该场景中引用的纹理、音频和 3D 模型等资源。
        // 加载场景
        _asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
        //设置后，加载完成了不自动跳转
        _asyncOperation.allowSceneActivation = false;
        // 加载场景时，将其进度分配给一个目标，用于平滑填充进度条
        while (_asyncOperation.progress < 0.9f)
        {
            //使用_asyncOperation加载，最大加载到0.9就会暂停
            _fillTarget = _asyncOperation.progress;
            //表示暂缓一帧
            yield return null;
        }

        // 当负载接近尾声时（永远不会到达），我们将其设置为100%
        _fillTarget = 1f;
        // 等待进度条填充完成
        while (_progressBarImage.fillAmount != _fillTarget)
        {
            //表示暂缓一帧
            yield return null;
        }

        // the load is now complete, we replace the bar with the complete animation
        LoadingComplete();
        yield return new WaitForSeconds(LoadCompleteDelay);

        // we fade to black
        MMFadeInEvent.Trigger(ExitFadeDuration, _tween);
        yield return new WaitForSeconds(ExitFadeDuration);

        // we switch to the new scene
        _asyncOperation.allowSceneActivation = true;
        LoadingSceneEvent.Trigger(_sceneToLoad, LoadingStatus.LoadTransitionComplete);
    }

    protected virtual void LoadingSetup()
    {
        LoadingCompleteAnimation.alpha = 0;
        _progressBarImage.fillAmount = 0f;
        LoadingText.text = _loadingTextValue;
    }

    protected virtual void LoadingComplete()
    {
        LoadingSceneEvent.Trigger(_sceneToLoad, LoadingStatus.InterpolatedLoadProgressComplete);
        LoadingCompleteAnimation.gameObject.SetActive(true);
        StartCoroutine(MMFade.FadeCanvasGroup(LoadingProgressBar, 0.1f, 0f));
        StartCoroutine(MMFade.FadeCanvasGroup(LoadingAnimation, 0.1f, 0f));
        StartCoroutine(MMFade.FadeCanvasGroup(LoadingCompleteAnimation, 0.1f, 1f));
    }

}
