using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState<T> : EnemyState where T : EnemyEntity
{
    private Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    protected bool flipAfterIdle;
    protected bool isPlayer;
    protected T enemy;
    
    public EnemyIdleState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO,T enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
    {
        this.enemy = enemy;
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isPlayer = EnemyEntity.IsPlayerDetected();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(0f);
        stateTimer = enemyDataSO.enemyData.idleTime;
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            Movement?.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }
}
