using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_TeleportState : EnemyState
{
    private Enemy_Boss enemy;
    public Enemy_Boss_TeleportState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Boss enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
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
            if (enemy.CanSpellCast())
                stateMachine.ChangeState(enemy.SpellCastState);
            else
                stateMachine.ChangeState(enemy.BattleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
