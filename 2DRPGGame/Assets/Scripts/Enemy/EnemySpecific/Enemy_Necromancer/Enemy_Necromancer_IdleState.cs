using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Necromancer_IdleState : EnemyIdleState<Enemy_Necromancer>
{
    public Enemy_Necromancer_IdleState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Necromancer enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
        else if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
