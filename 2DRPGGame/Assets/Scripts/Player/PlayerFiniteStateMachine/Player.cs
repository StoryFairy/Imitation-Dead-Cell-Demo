using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IMMEventListener<CorgiEngineEvent>
{
    #region 状态

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerStunnedState StunnedState { get; private set; }

    #endregion

    #region 组件

    public Core Core;
    public PlayerDataSO playerDataSO;
    public Animator Anim;
    public PlayerInputHandler InputHandler;
    public Rigidbody2D Rb;
    public CapsuleCollider2D coll;
    public Transform DashDirectionIndicator;
    public GameObject PlayerManager;
    public PlayerUI playerUI;

    #endregion

    #region 其他变量

    private Vector2 workspace;

    private Weapon primaryWeapon;
    public bool isStunned;
    
    
    [Header("OneWay")]
    public CompositeCollider2D platForm;
    public Transform Generated_Level;

    #endregion


    #region 生命周期函数

    private void Awake()
    {
        Instantiate(PlayerManager, Vector3.zero, Quaternion.identity);
        
        Core = GetComponentInChildren<Core>();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();

        playerUI = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();

        primaryWeapon.SetCore(Core);

        StateMachine = new PlayerStateMachine();
        
        //状态初始化
        IdleState = new PlayerIdleState(this, StateMachine, playerDataSO, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerDataSO, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerDataSO, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerDataSO, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerDataSO, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerDataSO, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerDataSO, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerDataSO, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerDataSO, "inAir");
        DashState = new PlayerDashState(this, StateMachine, playerDataSO, "inAir");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerDataSO, "attack", primaryWeapon);
        DeadState = new PlayerDeadState(this, StateMachine, playerDataSO, "dead");
        StunnedState = new PlayerStunnedState(this, StateMachine, playerDataSO, "stunned");

        primaryWeapon.weaponData = playerDataSO.playerData.weapon;
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        Generated_Level = GameObject.Find("Generated Level").transform;
        
        for (int i = 0; i < Generated_Level.childCount; i++)
        {
            Transform child = Generated_Level.GetChild(i);
            if (child.name == "Tilemaps")
            {
                platForm = child.Find("Platforms").GetComponent<CompositeCollider2D>();
            }
        }
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
        if (InputHandler.PauseInput)
        {
            TriggerPause();
        }

        if (InputHandler.NormInputY < 0)
        {
            StartCoroutine(OneWayPlatform());
        }
        
        if(isStunned)
            StateMachine.ChangeState(StunnedState);
    }

    private IEnumerator OneWayPlatform()
    {
        Physics2D.IgnoreCollision(coll, platForm, true);

        // 等待一定时间
        yield return new WaitForSeconds(0.5f);

        // 重新启用碰撞
        Physics2D.IgnoreCollision(coll, platForm, false);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnEnable()
    {
        this.MMEventStartListening<CorgiEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<CorgiEngineEvent>();
    }

    #endregion
    
    #region 其他函数

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    protected void TriggerPause()
    {
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.TogglePause);
        InputHandler.UsePauseInput();
    }

    #endregion
    

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        if (eventType.EventType == CorgiEngineEventTypes.PlayerDeath)
        {
            StateMachine.ChangeState(DeadState);
        }
    }
}
