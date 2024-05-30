using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    public PlayerDataSO playerData;
    public WeaponDataSO weaponData;
    [SerializeField] private float attackCounterResetCooldown;
    
    public bool CanEnterAttack { get; private set; }

    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= weaponData.NumberOfAttacks ? 0 : value;

    }

    public event Action OnEnter;
    public event Action OnExit;

    public Animator anim;

    public GameObject BaseGameObject { get; private set; }
    public GameObject WeaponSpriteGameObject { get; private set; }
    
    public Core Core { get; private set; }

    public AnimationEventHandler EventHandler { get; private set; }
    private int currentAttackCounter;

    private Timer attackCounterResetTimer;

    public void Enter()
    {
        attackCounterResetTimer.StopTimer();
        
        anim.SetBool("active", true);
        anim.SetInteger("counter", CurrentAttackCounter);
        
        OnEnter?.Invoke();
    }

    public void SetCore(Core core)
    {
        Core = core;
    }

    public void SetData(WeaponDataSO data)
    {
        weaponData = data;
    }
    
    public void SetCanEnterAttack(bool value) => CanEnterAttack = value;
    
    public void Exit()
    {
        anim.SetBool("active", false);

        CurrentAttackCounter++;
        attackCounterResetTimer.StartTimer();

        OnExit?.Invoke();
    }

    private void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
        anim = BaseGameObject.GetComponent<Animator>();

        EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    private void Update()
    {
        attackCounterResetTimer.Tick();
    }

    private void ResetAttackCounter() => CurrentAttackCounter = 0;

    private void OnEnable()
    {
        EventHandler.OnFinish += Exit;
        attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
    }

    private void OnDisable()
    {
        EventHandler.OnFinish -= Exit;
        attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
    }
}
