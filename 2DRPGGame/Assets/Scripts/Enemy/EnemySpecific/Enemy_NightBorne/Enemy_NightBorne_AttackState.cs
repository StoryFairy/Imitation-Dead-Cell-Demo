using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBorne_AttackState : EnemyAttackState<Enemy_NightBorne>
{
    public Enemy_NightBorne_AttackState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_NightBorne enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
        if (triggerCalled)
            stateMachine.ChangeState(enemy.BattleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
