using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState<T> : EnemyState where T : EnemyEntity
{
    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    protected T enemy;
    public EnemyStunnedState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, T enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
    {
        this.enemy = enemy;
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
    }
    
    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.enemyDataSO.enemyData.stunDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
