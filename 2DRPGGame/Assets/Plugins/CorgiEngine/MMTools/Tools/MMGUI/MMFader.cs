using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public struct MMFadeStopEvent
{
    public int ID;
    
    private static MMFadeStopEvent e;

    public static void Trigger(int id = 0)
    {
        e.ID = id;
        MMEventManager.TriggerEvent(e);
    }
}

public struct MMFadeEvent
{
    public int ID;
    public float Duration;
    public float TargetAlpha;
    public MMTweenType Curve;
    public bool IgnoreTimescale;
    static MMFadeEvent e;
    public static void Trigger(float duration, float targetAlpha, MMTweenType tween, int id = 0, bool ignoreTimescale = true)
    {
        e.ID = id;
        e.Duration = duration;
        e.TargetAlpha = targetAlpha;
        e.Curve = tween;
        e.IgnoreTimescale = ignoreTimescale;
        MMEventManager.TriggerEvent(e);
    }
}

public struct MMFadeInEvent
{
    public int ID;
    public float Duration;
    public MMTweenType Curve;
    public bool IgnoreTimescale;
    static MMFadeInEvent e;
    public Vector3 WorldPosition;

    public MMFadeInEvent(float duration, MMTweenType tween, int id = 0, bool ignoreTimescale = true,
        Vector3 worldPosition = new Vector3())
    {
        ID = id;
        Duration = duration;
        Curve = tween;
        IgnoreTimescale = ignoreTimescale;
        WorldPosition = worldPosition;
    }
    
    public static void Trigger(float duration, MMTweenType tween, int id = 0, bool ignoreTimescale = true,
        Vector3 worldPosition=new Vector3())
    {
        e.ID = id;
        e.Duration = duration;
        e.Curve = tween;
        e.IgnoreTimescale = ignoreTimescale;
        e.WorldPosition = worldPosition;
        MMEventManager.TriggerEvent(e);
    }
}

public struct MMFadeOutEvent
{
    public int ID;
    public float Duration;
    public MMTweenType Curve;
    public bool IgnoreTimescale;
    static MMFadeOutEvent e;
    public static void Trigger(float duration, MMTweenType tween, int id = 0, bool ignoreTimescale = true)
    {
        e.ID = id;
        e.Duration = duration;
        e.Curve = tween;
        e.IgnoreTimescale = ignoreTimescale;
        MMEventManager.TriggerEvent(e);
    }
}



[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
[AddComponentMenu("More Mountains/Tools/GUI/MMFader")]
public class MMFader : MonoBehaviour, IMMEventListener<MMFadeEvent>, IMMEventListener<MMFadeInEvent>,
    IMMEventListener<MMFadeOutEvent>, IMMEventListener<MMFadeStopEvent>
{
    public enum ForcedInitStates
    {
        None,
        Active,
        Inactive
    }

    [Header("识别码")] public int ID;

    [Header("不透明度")] public float InactiveAlpha = 0f;
    public float ActiveAlpha = 1f;

    public ForcedInitStates ForcedInitState = ForcedInitStates.Inactive;

    [Header("计时")] public float DefaultDuration = 0.2f;
    public MMTweenType DefaultTween = new MMTweenType(MMTween.MMTweenCurve.LinearTween);
    public bool IgnoreTimescale = true;

    [Header("Interaction")] public bool ShouldBlockRaycasts = false;

    protected CanvasGroup _canvasGroup;
    protected Image _image;

    [Header("Debug")] [MMInspectorButton("FadeIn1Second")]
    public bool FadeIn1SecondButton;

    [MMInspectorButton("FadeOut1Second")] public bool FadeOut1SecondButton;
    [MMInspectorButton("DefaultFade")] public bool DefaultFadeButton;
    [MMInspectorButton("ResetFader")] public bool ResetFaderButton;
    [MMInspectorButton("StartFader")] public bool StartFaderButton;

    protected float _initialAlpha; //初始化目标透明值
    protected float _currentTargetAlpha; //当前目标透明值
    protected float _currentDuration; //淡入淡出时间
    protected MMTweenType _currentCurve; //当前使用的曲线

    protected bool _fading = false;
    protected float _fadeStartedAt;
    protected bool _frameCountOne;

    protected virtual void StartFader()
    {
        StartFading(ActiveAlpha, InactiveAlpha, DefaultDuration, DefaultTween, ID, IgnoreTimescale);
    }

    protected virtual void ResetFader()
    {
        _canvasGroup.alpha = InactiveAlpha;
    }


    /// <summary>
    /// Test method triggered by an inspector button
    /// </summary>
    protected virtual void DefaultFade()
    {
        if (ForcedInitState == ForcedInitStates.Inactive)
        {
            MMFadeEvent.Trigger(DefaultDuration, InactiveAlpha, DefaultTween, ID);
        }
        else if (ForcedInitState == ForcedInitStates.Active)
        {
            MMFadeEvent.Trigger(DefaultDuration, ActiveAlpha, DefaultTween, ID);
        }
    }

    /// <summary>
    /// Test method triggered by an inspector button
    /// </summary>
    protected virtual void FadeIn1Second()
    {
        MMFadeInEvent.Trigger(1f, new MMTweenType(MMTween.MMTweenCurve.LinearTween), ID);
    }

    /// <summary>
    /// Test method triggered by an inspector button
    /// </summary>
    protected virtual void FadeOut1Second()
    {
        MMFadeOutEvent.Trigger(1f, new MMTweenType(MMTween.MMTweenCurve.LinearTween), ID);
    }

    private void Awake()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _image = GetComponent<Image>();


        if (ForcedInitState == ForcedInitStates.Inactive)
        {
            _canvasGroup.alpha = InactiveAlpha;
            _image.enabled = false;
        }
        else if (ForcedInitState == ForcedInitStates.Active)
        {
            _canvasGroup.alpha = ActiveAlpha;
            _image.enabled = true;
        }
    }

    protected virtual void Update()
    {
        if (_canvasGroup == null) return;
        {
            if (_fading)
            {
                Fade();
            }
        }
    }

    protected virtual void Fade()
    {
        float currentTime = IgnoreTimescale ? Time.unscaledTime : Time.time;
        //Scene刚刚打开，所以现在是画面的第一帧
        if (_frameCountOne)
        {
            //在Scenes刚刚加载的几帧里面不要触发Fader，不然场景加载的前几帧黑屏
            //和Fader混合在一起
            if (Time.frameCount <= 2)
            {
                //这里几乎不会进去
                _canvasGroup.alpha = _initialAlpha;
                return;
            }

            _fadeStartedAt = IgnoreTimescale ? Time.unscaledTime : Time.time;
            currentTime = _fadeStartedAt;
            _frameCountOne = false;
        }

        float endTime = _fadeStartedAt + _currentDuration;
        if (currentTime - _fadeStartedAt < _currentDuration)
        {
            float result = MMTween.Tween(currentTime, _fadeStartedAt, endTime, _initialAlpha, _currentTargetAlpha,
                _currentCurve);
            _canvasGroup.alpha = result;
        }
        else
        {
            StopFading();
        }
    }

    protected virtual void StopFading()
    {
        _canvasGroup.alpha = _currentTargetAlpha;
        _fading = false;
        if (_canvasGroup.alpha == InactiveAlpha)
        {
            DisableFader();
        }

    }

    protected virtual void DisableFader()
    {
        _image.enabled = false;
        if (ShouldBlockRaycasts)
        {
            _canvasGroup.blocksRaycasts = false;
        }
    }


    protected virtual void EnableFader()
    {
        _image.enabled = true;
        if (ShouldBlockRaycasts)
        {
            _canvasGroup.blocksRaycasts = true;
        }
    }

    protected virtual void StartFading(float initialAlpha, float endAlpha, float duration, MMTweenType curve, int id,
        bool ignoreTimeScale)
    {
        //传入的id 不是当前GameObject的ID的话，就不要进行操作
        //由于我们之后要通过事件触发来控制StartFading，所以这个逻辑需要的
        if (id != ID) return;

        IgnoreTimescale = ignoreTimeScale;
        EnableFader();
        _fading = true;
        _initialAlpha = initialAlpha;
        _currentTargetAlpha = endAlpha;
        //就算游戏控制了TimeScale实现慢镜头，也对我们Fader不做影响
        _fadeStartedAt = IgnoreTimescale ? Time.unscaledTime : Time.time;
        _currentCurve = curve;
        _currentDuration = duration;
        if (Time.frameCount == 1)
        {
            _frameCountOne = true;
        }
    }

    public void OnMMEvent(MMFadeEvent eventType) //当前透明度到目标透明度
    {
        _currentTargetAlpha = (eventType.TargetAlpha == -1) ? ActiveAlpha : eventType.TargetAlpha;
        StartFading(_canvasGroup.alpha, _currentTargetAlpha, eventType.Duration, eventType.Curve, eventType.ID,
            eventType.IgnoreTimescale);
    }

    public void OnMMEvent(MMFadeInEvent eventType) //淡入
    {
        StartFading(InactiveAlpha, ActiveAlpha, eventType.Duration, eventType.Curve, eventType.ID,
            eventType.IgnoreTimescale);
    }

    public void OnMMEvent(MMFadeOutEvent eventType) //淡出
    {
        StartFading(ActiveAlpha, InactiveAlpha, eventType.Duration, eventType.Curve, eventType.ID,
            eventType.IgnoreTimescale);
    }

    public void OnMMEvent(MMFadeStopEvent eventType) //停止淡入淡出
    {
        if (eventType.ID == ID)
        {
            _fading = false;
        }
    }

    protected virtual void OnEnable()
    {
        this.MMEventStartListening<MMFadeEvent>();
        this.MMEventStartListening<MMFadeStopEvent>();
        this.MMEventStartListening<MMFadeInEvent>();
        this.MMEventStartListening<MMFadeOutEvent>();
    }

    protected virtual void OnDisable()
    {
        this.MMEventStopListening<MMFadeEvent>();
        this.MMEventStopListening<MMFadeStopEvent>();
        this.MMEventStopListening<MMFadeInEvent>();
        this.MMEventStopListening<MMFadeOutEvent>();
    }
}
