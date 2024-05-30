using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBorne_MoveState : EnemyMoveState<Enemy_NightBorne>
{
    public Enemy_NightBorne_MoveState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_NightBorne enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayer)
        {
            stateMachine.ChangeState(enemy.BattleState);
        }

        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
