using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBorne_DeadState : EnemyDeadState<Enemy_NightBorne>
{
    public Enemy_NightBorne_DeadState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, Enemy_NightBorne enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AttackTrigger()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(enemy.transform.position,
            enemyDataSO.enemyData.attackRadius * 4, enemyDataSO.enemyData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                player.isStunned = true;
                if (damageable != null)
                {
                    damageable.Damage(enemyDataSO.enemyData.attackDamage * enemyDataSO.enemyData.baseAttackMultiplier *
                                      5);
                }

                player.playerUI.UpdateHealth();
            }
        }
    }
}
