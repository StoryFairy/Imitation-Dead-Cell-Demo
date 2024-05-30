using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBorne : EnemyEntity
{
    public Enemy_NightBorne_IdleState IdleState { get; private set; }
    public Enemy_NightBorne_MoveState MoveState { get; private set; }
    public Enemy_NightBorne_BattleState BattleState { get; private set; }
    public Enemy_NightBorne_AttackState AttackState { get; private set; }
    public Enemy_NightBorne_StunnedState StunnedState { get; private set; }
    public Enemy_NightBorne_DeadState DeadState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        IdleState = new Enemy_NightBorne_IdleState(this, stateMachine, "Idle", enemyDataSO, this);
        MoveState = new Enemy_NightBorne_MoveState(this, stateMachine, "Move", enemyDataSO, this);
        BattleState = new Enemy_NightBorne_BattleState(this, stateMachine, "Move", enemyDataSO, this);
        AttackState = new Enemy_NightBorne_AttackState(this, stateMachine, "Attack", enemyDataSO, this);
        StunnedState = new Enemy_NightBorne_StunnedState(this, stateMachine, "Stunned", enemyDataSO, this);
        DeadState = new Enemy_NightBorne_DeadState(this, stateMachine, "Dead", enemyDataSO, this);
    }
    
    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    public override void Update()
    {
        base.Update();

        if (isStunned)
        {
            stateMachine.ChangeState(StunnedState);
            isStunned = false;
        }
    }
}
