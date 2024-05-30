using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slime_StunnedState : EnemyStunnedState<Enemy_Slime>
{
    public Enemy_Slime_StunnedState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Slime enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
