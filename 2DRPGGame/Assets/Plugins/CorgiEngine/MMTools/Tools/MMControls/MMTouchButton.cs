using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Rect))]
[RequireComponent(typeof(CanvasGroup))]
[Serializable]
[AddComponentMenu("More Mountains/Tools/Controls/MMTouchButton")]
public class MMTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler,
    IPointerEnterHandler, ISubmitHandler
{
    public enum ButtonStates
    {
        Off,
        ButtonDown,
        ButtonPressed,
        ButtonUp,
        Disabled
    }

    [Header("Binding")] public UnityEvent ButtonPressedFirstTime;
    public UnityEvent ButtonReleased;
    public UnityEvent ButtonPressed;

    [Header("Sprite Swap")]
    [MMInformation("如果想要不同的sprite和不同的color，可以在这里定义禁用状态和按下状态", MMInformationAttribute.InformationType.Info, false)]
    public Sprite DisabledSprite;

    public bool DisabledChangeColor = false;
    public Color DisabledColor = Color.white;
    public Sprite PressedSprite;
    public bool PressedChangeColor = false;
    public Color PressedColor = Color.white;
    public Sprite HighlightedSprite;
    public bool HighlightedChangeColor = false;
    public Color HighlightedColor = Color.white;

    [Header("Opacity")] [MMInformation("在按下、空闲或禁用按钮时为其设置不同的不透明度", MMInformationAttribute.InformationType.Info, false)]
    /// 按下按钮时应用于Canvas的新透明度
    public float PressedOpacity = 1f;

    public float IdleOpacity = 1f;
    public float DisabledOpacity = 1f;

    [Header("Delays")] [MMInformation("指定初始按下按钮和释放按钮时应用的延迟", MMInformationAttribute.InformationType.Info, false)]
    public float PressedFirstTimeDelay = 0f;

    public float ReleasedDelay = 0f;

    [Header("Buffer")] public float BufferDuration = 0f;

    [Header("Animation")] [MMInformation("绑定动画，并为各种状态指定动画参数名称。", MMInformationAttribute.InformationType.Info, false)]
    public Animator Animator;

    public string IdleAnimationParameterName = "Idle";
    public string DisabledAnimationParameterName = "Disabled";
    public string PressedAnimationParameterName = "Pressed";

    [Header("Mouse Mode")]
    [MMInformation("true需要按下按钮才能触发它，false只要悬停就会触发它", MMInformationAttribute.InformationType.Info, false)]
    public bool MouseMode = false;

    public bool ReturnToInitialSpriteAutomatically { get; set; } //自动返回初始sprite

    public ButtonStates CurrentState { get; protected set; }

    protected bool _zonePressed = false;
    protected CanvasGroup _canvasGroup;
    protected float _initialOpacity;
    protected Animator _animator;
    protected Image _image;
    protected Sprite _initialSprite;
    protected Color _initialColor;
    protected float _lastClickTimestamp = 0f;
    protected Selectable _selectable; //用于实现按钮悬停高亮

    protected virtual void Awake()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        ReturnToInitialSpriteAutomatically = true;
        _selectable = GetComponent<Selectable>();
        _image = GetComponent<Image>();
        if (_image != null)
        {
            _initialColor = _image.color;
            _initialSprite = _image.sprite;
        }

        _animator = GetComponent<Animator>();
        if (Animator != null)
        {
            _animator = Animator;
        }

        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup != null)
        {
            _initialOpacity = IdleOpacity;
            _canvasGroup.alpha = _initialOpacity;
            _initialOpacity = _canvasGroup.alpha;
        }

        ResetButton();
    }

    protected virtual void Update()
    {
        switch (CurrentState)
        {
            case ButtonStates.Off:
                SetOpacity(IdleOpacity);
                if ((_image != null) && (ReturnToInitialSpriteAutomatically))
                {
                    _image.color = _initialColor;
                    _image.sprite = _initialSprite;
                }

                if (_selectable != null)
                {
                    _selectable.interactable = true;
                    if (EventSystem.current.currentSelectedGameObject == this.gameObject)
                    {
                        if ((_image != null) && HighlightedChangeColor)
                        {
                            _image.color = HighlightedColor;
                        }

                        if (HighlightedSprite != null)
                        {
                            _image.sprite = HighlightedSprite;
                        }
                    }
                }

                break;
            case ButtonStates.Disabled:
                SetOpacity(DisabledOpacity);
                if (_image != null)
                {
                    if (DisabledSprite != null)
                    {
                        _image.sprite = DisabledSprite;
                    }

                    if (DisabledChangeColor)
                    {
                        _image.color = DisabledColor;
                    }
                }

                if (_selectable != null)
                {
                    _selectable.interactable = false;
                }

                break;
            case ButtonStates.ButtonDown:
                break;
            case ButtonStates.ButtonPressed:
                SetOpacity(PressedOpacity);
                OnPointerPressed();
                if (_image != null)
                {
                    if (PressedSprite != null)
                    {
                        _image.sprite = PressedSprite;
                    }

                    if (PressedChangeColor)
                    {
                        _image.color = PressedColor;
                    }
                }

                break;
            case ButtonStates.ButtonUp:
                break;

        }

        UpdateAnimatorStates();
    }

    protected virtual void LateUpdate()
    {
        if (CurrentState == ButtonStates.ButtonUp)
        {
            CurrentState = ButtonStates.Off;
        }

        if (CurrentState == ButtonStates.ButtonDown)
        {
            CurrentState = ButtonStates.ButtonPressed;
        }
    }

    protected virtual void ResetButton()
    {
        SetOpacity(_initialOpacity);
        CurrentState = ButtonStates.Off;
    }

    protected virtual void SetOpacity(float newOpacity)
    {
        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = newOpacity;
        }
    }

    protected virtual void OnEnable()
    {
        ResetButton();
    }

    private void OnDisable()
    {
        bool wasActive = CurrentState != ButtonStates.Off && CurrentState != ButtonStates.Disabled;
        DisableButton();
        CurrentState = ButtonStates.Off; // cause it's what is tested to StopInput (for weapon by example)
        if (wasActive)
        {
            ButtonStateChange?.Invoke(PointerEventData.FramePressState.Released, null);
            ButtonReleased?.Invoke();
        }
    }

    public virtual void DisableButton()
    {
        CurrentState = ButtonStates.Disabled;
    }

    public virtual void EnableButton()
    {
        if (CurrentState == ButtonStates.Disabled)
        {
            CurrentState = ButtonStates.Off;
        }
    }

    public event System.Action<PointerEventData.FramePressState, PointerEventData> ButtonStateChange;
    
    public void OnPointerUp(PointerEventData data) //鼠标松开触发
    {
        if (CurrentState != ButtonStates.ButtonPressed && CurrentState != ButtonStates.ButtonDown)
        {
            return;
        }

        CurrentState = ButtonStates.ButtonUp;
        ButtonStateChange?.Invoke(PointerEventData.FramePressState.Released, data);
        if ((Time.timeScale != 0) && (ReleasedDelay > 0))
        {
            Invoke("InvokeReleased", ReleasedDelay);
        }
        else
        {
            ButtonReleased.Invoke();
        }
    }
    
    public void OnPointerDown(PointerEventData data) //鼠标按下触发
    {
        if (Time.time - _lastClickTimestamp < BufferDuration)
        {
            return;
        }

        if (CurrentState != ButtonStates.Off)
        {
            return;
        }

        CurrentState = ButtonStates.ButtonDown;
        _lastClickTimestamp = Time.time;
        ButtonStateChange?.Invoke(PointerEventData.FramePressState.Pressed, data);
        if ((Time.timeScale != 0) && (PressedFirstTimeDelay > 0))
        {
            Invoke("InvokePressedFirstTime", PressedFirstTimeDelay);
        }
        else
        {
            ButtonPressedFirstTime.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData data) //鼠标移出触发
    {
        if (!MouseMode)
        {
            OnPointerUp(data);
        }
    }

    public void OnPointerEnter(PointerEventData data) //鼠标移入触发
    {
        if (!MouseMode)
        {
            OnPointerDown(data);
        }
    }

    protected virtual void InvokePressedFirstTime()
    {
        if (ButtonPressedFirstTime != null)
        {
            ButtonPressedFirstTime.Invoke();
        }
    }

    protected virtual void UpdateAnimatorStates()
    {
        if (_animator == null)
        {
            return;
        }

        if (DisabledAnimationParameterName != null)
        {
            _animator.SetBool(DisabledAnimationParameterName, (CurrentState == ButtonStates.Disabled));
        }

        if (PressedAnimationParameterName != null)
        {
            _animator.SetBool(PressedAnimationParameterName, (CurrentState == ButtonStates.ButtonPressed));
        }

        if (IdleAnimationParameterName != null)
        {
            _animator.SetBool(IdleAnimationParameterName, (CurrentState == ButtonStates.Off));
        }
    }

    protected virtual void InvokeReleased()
    {
        if (ButtonReleased != null)
        {
            ButtonReleased.Invoke();
        }
    }

    public virtual void OnPointerPressed()
    {
        CurrentState = ButtonStates.ButtonPressed;
        if (ButtonPressed != null)
        {
            ButtonPressed.Invoke();
        }
    }

    public void OnSubmit(BaseEventData data) //提交触发
    {

    }
}