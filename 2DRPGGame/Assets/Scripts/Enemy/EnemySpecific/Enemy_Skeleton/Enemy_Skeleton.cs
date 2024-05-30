using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : EnemyEntity
{
    public Enemy_Skeleton_IdleState IdleState { get; private set; }
    public Enemy_Skeleton_MoveState MoveState { get; private set; }
    public Enemy_Skeleton_BattleState BattleState { get; private set; }
    public Enemy_Skeleton_AttackState AttackState { get; private set; }
    public Enemy_Skeleton_StunnedState StunnedState { get; private set; }
    public Enemy_Skeleton_DeadState DeadState { get; private set; }



    public override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_Skeleton_IdleState(this, stateMachine, "Idle", enemyDataSO, this);
        MoveState = new Enemy_Skeleton_MoveState(this, stateMachine, "Move", enemyDataSO, this);
        BattleState = new Enemy_Skeleton_BattleState(this, stateMachine, "Move", enemyDataSO, this);
        AttackState = new Enemy_Skeleton_AttackState(this, stateMachine, "Attack", enemyDataSO, this);
        StunnedState = new Enemy_Skeleton_StunnedState(this, stateMachine, "Stunned", enemyDataSO, this);
        DeadState = new Enemy_Skeleton_DeadState(this, stateMachine, "Dead", enemyDataSO, this);
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