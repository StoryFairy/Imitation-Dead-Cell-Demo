using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton_StunnedState : EnemyStunnedState<Enemy_Skeleton>
{
    public Enemy_Skeleton_StunnedState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Skeleton enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
        if(enemy.isDead)
            stateMachine.ChangeState(enemy.DeadState);
        else if(stateTimer<0)
            stateMachine.ChangeState(enemy.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
