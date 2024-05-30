using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy_Boss : EnemyEntity
{
    [Header("Spell cast details")] 
    [SerializeField] public GameObject Spell;
    [SerializeField] public Enemy_Boss_Spell_Controller SpellController;
    private float lastTimeCast;
    [SerializeField] private float spellCastCooldown;
    
    [Header("Teleport details")]
    [SerializeField] private CapsuleCollider2D cd;
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    [HideInInspector] public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;

    public BossUI bossUI;
    
    [HideInInspector] public float StunnedValue;
    
    public Enemy_Boss_IdleState IdleState { get; private set; }
    public Enemy_Boss_BattleState BattleState { get; private set; }
    public Enemy_Boss_AttackState AttackState { get; private set; }
    public Enemy_Boss_DeadState DeadState { get; private set; }
    public Enemy_Boss_SpellCastState SpellCastState { get; private set; }
    public Enemy_Boss_TeleportState TeleportState { get; private set; }
    public Enemy_Boss_StunnedState StunnedState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_Boss_IdleState(this, stateMachine, "Idle", enemyDataSO, this);
        BattleState = new Enemy_Boss_BattleState(this, stateMachine, "Move", enemyDataSO, this);
        AttackState = new Enemy_Boss_AttackState(this, stateMachine, "Attack", enemyDataSO, this);
        DeadState = new Enemy_Boss_DeadState(this, stateMachine, "Dead", enemyDataSO, this);
        SpellCastState = new Enemy_Boss_SpellCastState(this, stateMachine, "SpellCast", enemyDataSO, this);
        TeleportState = new Enemy_Boss_TeleportState(this, stateMachine, "Teleport", enemyDataSO, this);
        StunnedState = new Enemy_Boss_StunnedState(this, stateMachine, "Stunned", enemyDataSO, this);
    }

    private void Start()
    {
        Movement.Flip();
        stateMachine.Initialize(IdleState);
    }

    public override void Update()
    {
        base.Update();
        if(isDead)
            stateMachine.ChangeState(DeadState);
        
        if (isStunned)
        {
            StunnedValue += 5;
            if (StunnedValue >= 100)
            {
                stateMachine.ChangeState(StunnedState);
            }
            isStunned = false;
        }
    }
    
    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x,
            transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if (!GroundBelow() || SomethingIsAround())
        {
            FindPosition();
        }
    }

    public bool CanTeleport()
    {
        if (GameManager.Instance.RandomNumber() <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }

        return false;
    }

    public bool CanSpellCast()
    {
        if (Time.time >= lastTimeCast + spellCastCooldown)
        {
            lastTimeCast = Time.time;
            return true;
        }

        return false;
    }


    private RaycastHit2D GroundBelow() =>
        Physics2D.Raycast(transform.position, Vector2.down, 100, CollisionSenses.WhatIsGround);

    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0,
        CollisionSenses.WhatIsGround);
}
