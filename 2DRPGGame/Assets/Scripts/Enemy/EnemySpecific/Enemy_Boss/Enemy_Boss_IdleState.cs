using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_IdleState : EnemyIdleState<Enemy_Boss>
{
    public Enemy_Boss_IdleState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Boss enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
