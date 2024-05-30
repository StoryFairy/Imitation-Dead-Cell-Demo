using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState<T> : EnemyState where T : EnemyEntity
{
    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    protected T enemy;

    public EnemyAttackState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, T enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
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
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityZero();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AttackTrigger()
    {
        base.AttackTrigger();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(EnemyEntity.attackPosition.position,
            enemyDataSO.enemyData.attackRadius, enemyDataSO.enemyData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            Player player = collider.GetComponent<Player>();

            if (player != null)
            {
                player.isStunned = true;
                if (damageable != null)
                {
                    damageable.Damage(enemyDataSO.enemyData.attackDamage * enemyDataSO.enemyData.baseAttackMultiplier);
                }

                player.playerUI.UpdateHealth();
            }
        }
    }
}
