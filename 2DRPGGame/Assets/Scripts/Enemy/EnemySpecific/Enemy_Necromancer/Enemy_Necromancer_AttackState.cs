using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Necromancer_AttackState : EnemyAttackState<Enemy_Necromancer>
{
    public Enemy_Necromancer_AttackState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Necromancer enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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

    public override void AttackTrigger()
    {
        GameObject.Instantiate(enemy.Fireball, enemy.transform.position, Quaternion.identity);
    }
}
