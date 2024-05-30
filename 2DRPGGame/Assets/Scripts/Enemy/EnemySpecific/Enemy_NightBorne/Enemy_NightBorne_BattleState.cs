using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBorne_BattleState : EnemyBattleState<Enemy_NightBorne>
{
    public Enemy_NightBorne_BattleState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_NightBorne enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isIdle = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (canAttack)
            stateMachine.ChangeState(enemy.AttackState);
        else if(isDetectingLedge)
            stateMachine.ChangeState(enemy.MoveState);
        else if (enemy.isIdle)
            stateMachine.ChangeState(enemy.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
