using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMSpriteReplace : MonoBehaviour
{
    [Header("Sprites")]
    [MMInformation("Add this to an Image or a SpriteRenderer to be able to swap between two sprites.",
        MMInformationAttribute.InformationType.Info, false)]

    /// the sprite to use when in the "on" state
    public Sprite OnSprite;

    /// the sprite to use when in the "off" state
    public Sprite OffSprite;


    [Header("Start settings")]
    /// if this is true, the button will start if "on" state
    public bool StartsOn = true;

    [Header("Debug")] [MMInspectorButton("Swap")]
    public bool SwapButton;

    [MMInspectorButton("SwitchToOffSprite")]
    public bool SwitchToOffSpriteButton;

    [MMInspectorButton("SwitchToOnSprite")]
    public bool SwitchToOnSpriteButton;

    /// the current state of the button
    public bool CurrentValue
    {
        get { return (_image.sprite == OnSprite); }
    }

    protected Image _image;
    protected SpriteRenderer _spriteRenderer;
    protected MMTouchButton _mmTouchButton;

    protected virtual void Start()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        // grabs components
        _image = GetComponent<Image>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // grabs button
        _mmTouchButton = GetComponent<MMTouchButton>();
        if (_mmTouchButton != null)
        {
            Debug.Log("MMSpriteReplace----Initialization----1");
            _mmTouchButton.ReturnToInitialSpriteAutomatically = false;
        }

        // handles start
        if ((OnSprite == null) || (OffSprite == null))
        {
            Debug.Log("MMSpriteReplace----Initialization----2");
            return;
        }

        if (_image != null)
        {
            if (StartsOn)
            {
                _image.sprite = OnSprite;
            }
            else
            {
                Debug.Log("MMSpriteReplace----Initialization----4");
                _image.sprite = OffSprite;
            }
        }

        if (_spriteRenderer != null)
        {
            if (StartsOn)
            {
                Debug.Log("MMSpriteReplace----Initialization----5");
                _spriteRenderer.sprite = OnSprite;
            }
            else
            {
                Debug.Log("MMSpriteReplace----Initialization----6");
                _spriteRenderer.sprite = OffSprite;
            }
        }
    }

    public virtual void Swap()
    {
        if (_image != null)
        {
            if (_image.sprite != OnSprite)
            {
                SwitchToOnSprite();
            }
            else
            {
                SwitchToOffSprite();
            }
        }

        if (_spriteRenderer != null)
        {
            Debug.Log("MMSpriteReplace----Swap");
            if (_spriteRenderer.sprite != OnSprite)
            {
                SwitchToOnSprite();
            }
            else
            {
                SwitchToOffSprite();
            }
        }
    }

    public virtual void SwitchToOffSprite()
    {
        if ((_image == null) && (_spriteRenderer == null))
        {
            Debug.Log("MMSpriteReplace----SwitchToOffSprite---1");
            return;
        }

        if (OffSprite == null)
        {
            Debug.Log("MMSpriteReplace----SwitchToOffSprite0----2");
            return;
        }

        SpriteOff();
    }

    protected virtual void SpriteOff()
    {
        if (_image != null)
        {
            _image.sprite = OffSprite;
        }

        if (_spriteRenderer != null)
        {
            Debug.Log("MMSpriteReplace----SpriteOff----3");
            _spriteRenderer.sprite = OffSprite;
        }
    }

    public virtual void SwitchToOnSprite()
    {
        if ((_image == null) && (_spriteRenderer == null))
        {
            Debug.Log("MMSpriteReplace----SwitchToOnSprite---1");
            return;
        }

        if (OnSprite == null)
        {
            Debug.Log("MMSpriteReplace----SwitchToOnSprite---2");
            return;
        }

        SpriteOn();
    }

    protected virtual void SpriteOn()
    {
        if (_image != null)
        {
            _image.sprite = OnSprite;
        }

        if (_spriteRenderer != null)
        {
            Debug.Log("MMSpriteReplace----SpriteOn");
            _spriteRenderer.sprite = OnSprite;
        }
    }
}
