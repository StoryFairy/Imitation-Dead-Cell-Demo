using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool AttackInputs { get; private set; }
    public bool InteractInput { get; private set; }
    public bool PauseInput { get; private set; }

    [SerializeField] private float inputHoldTime = 0.5f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Paused)
        {
            return;
        }
        
        if (context.started)
        {
            AttackInputs = true;
        }

        if (context.canceled)
        {
            AttackInputs = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Paused)
        {
            return;
        }
        
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Paused)
        {
            return;
        }
        
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Paused)
        {
            return;
        }
        
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Paused)
        {
            return;
        }
        
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Paused)
        {
            return;
        }
        
        RawDashDirectionInput = context.ReadValue<Vector2>();
        if(cam!=null)
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        
        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }


    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Paused)
        {
            return;
        }

        if (context.started)
        {
            InteractInput = true;
        }
        else if (context.canceled)
        {
            InteractInput = false;
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)  
    {
        if (context.started)
        {
            PauseInput = true;
        }
    }

    public void UseJumpInput()
    {
        JumpInput = false;
    }

    public void UseDashInput()
    {
        DashInput = false;
    }

    public void UsePauseInput()
    {
        PauseInput = false;
    }

    public void UseInteractInput()
    {
        InteractInput = false;
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}
