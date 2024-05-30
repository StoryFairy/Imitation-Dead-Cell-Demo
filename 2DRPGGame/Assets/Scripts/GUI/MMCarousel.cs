using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MMCarousel : MonoBehaviour
{
    [Header("Binding")]
    /// 包含所有转盘元素的布局组
    public HorizontalLayoutGroup Content;

    public Camera UICamera;

    [Header("Optional Buttons Binding")] public MMTouchButton LeftButton;
    public MMTouchButton RightButton;

    [Header("Carousel Setup")]
    /// 当前索引
    public int CurrentIndex = 0;

    /// 转盘中每次应移动的项目数
    public int Pagination = 1;

    /// 到达时将停止移动的距离百分比
    public float ThresholdInPercent = 1f;

    [Header("Speed")]
    /// 转盘移动的持续时间（秒）
    public float MoveDuration = 0.05f;

    [Header("Focus")]
    /// 在此处绑定最初应该具有焦点的转盘项目
    public GameObject InitialFocus;


    //protected float ElementWidth { get { return (Content.minWidth - (Content.spacing * (Content.flexibleWidth - 1))) / Content.flexibleWidth; }}
    protected float _elementWidth;
    protected int _contentLength = 0;
    protected float _spacing;
    protected Vector2 _initialPosition;
    protected RectTransform _rectTransform;

    protected bool _lerping = false;
    protected float _lerpStartedTimestamp;
    protected Vector2 _startPosition;
    protected Vector2 _targetPosition;

    private void Awake()
    {
        Debug.Log(_contentLength);
    }

    protected void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        _rectTransform = Content.gameObject.GetComponent<RectTransform>();
        _initialPosition = _rectTransform.anchoredPosition;

        // we compute the Content's element width
        _contentLength = 0;
        foreach (Transform tr in Content.transform)
        {
            _elementWidth = tr.gameObject.MMGetComponentNoAlloc<RectTransform>().sizeDelta.x; //宽度
            _contentLength++;
        }

        _spacing = Content.spacing;
        // we position our carousel at the desired initial index
        _rectTransform.anchoredPosition = DeterminePosition();
        if (InitialFocus != null)
        {
            EventSystem.current.SetSelectedGameObject(InitialFocus, null);
        }
    }

    public virtual void MoveLeft()
    {
        if (!CanMoveLeft())
        {
            return;
        }
        else
        {
            CurrentIndex -= Pagination;
            MoveToCurrentIndex();
        }
    }

    public virtual void MoveRight()
    {
        if (!CanMoveRight())
        {
            return;
        }
        else
        {
            CurrentIndex += Pagination;
            MoveToCurrentIndex();
        }
    }

    protected virtual void MoveToCurrentIndex()
    {
        _startPosition = _rectTransform.anchoredPosition;
        _targetPosition = DeterminePosition();
        _lerping = true;
        _lerpStartedTimestamp = Time.time;
    }

    protected virtual Vector2 DeterminePosition()
    {
        return _initialPosition - (Vector2.right * CurrentIndex * (_elementWidth + _spacing));
    }

    public virtual bool CanMoveLeft()
    {
        return (CurrentIndex - Pagination >= 0);
    }

    public virtual bool CanMoveRight()
    {
        return (CurrentIndex + Pagination < _contentLength);
    }

    protected virtual void Update()
    {
        if (_lerping)
            LerpPosition();
        HandleButton();
        HandleFocus();
    }

    protected virtual void HandleFocus()
    {
        if (!_lerping && Time.timeSinceLevelLoad > 0.5f)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (UICamera.WorldToScreenPoint(EventSystem.current.currentSelectedGameObject.transform.position).x < 0)
                {
                    MoveLeft();
                }
                if (UICamera.WorldToScreenPoint(EventSystem.current.currentSelectedGameObject.transform.position).x > Screen.width)
                {
                    MoveRight();
                      
                }
                   
            }
        }
    }

    protected virtual void HandleButton()
    {
        if (LeftButton != null)
        {
            if (CanMoveLeft())
            {
                LeftButton.EnableButton();
            }
            else
            {
                LeftButton.DisableButton();
            }
        }

        if (RightButton != null)
        {
            if (CanMoveRight())
            {
                RightButton.EnableButton();
            }
            else
            {
                RightButton.DisableButton();
            }

        }
    }

    protected virtual void LerpPosition()
    {
        float timeSinceStarted = Time.time - _lerpStartedTimestamp;
        float percentageComplete = timeSinceStarted / MoveDuration;
        _rectTransform.anchoredPosition = Vector2.Lerp(_startPosition, _targetPosition, percentageComplete);
        if (percentageComplete >= ThresholdInPercent)
            _lerping = false;
    }
}
