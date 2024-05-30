using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_AttackState : EnemyAttackState<Enemy_Boss>
{
    public Enemy_Boss_AttackState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Boss enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.chanceToTeleport += 5;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (triggerCalled)
        {
            if (enemy.CanTeleport())
                stateMachine.ChangeState(enemy.TeleportState);
            else
                stateMachine.ChangeState(enemy.BattleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
