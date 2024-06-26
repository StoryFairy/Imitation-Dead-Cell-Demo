using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_BattleState : EnemyBattleState<Enemy_Boss>
{
    public Enemy_Boss_BattleState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Boss enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
        enemy.isIdle = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (canAttack)
            stateMachine.ChangeState(enemy.AttackState);
        else if (enemy.isIdle)
            stateMachine.ChangeState(enemy.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
