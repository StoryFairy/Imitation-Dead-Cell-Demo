using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleState<T> : EnemyState where T : EnemyEntity
{
    private Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected Transform player;

    protected bool isDetectingLedge;
    protected bool canAttack;
    protected T enemy;

    public EnemyBattleState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, T enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (CollisionSenses)
        {
            isDetectingLedge = CollisionSenses.LedgeVertical;
        }
    }

    public override void Enter()
    {
        base.Enter();
        if (enemy.isPlayerExist())
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    public override void Exit()
    {
        base.Exit();
        canAttack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.IsPlayerDetected())
        {
            if (enemy.IsPlayerDetected().distance - 8 < enemy.enemyDataSO.enemyData.attackDistance)
            {
                canAttack = CanAttack();
            }
        }

        if(player!=null)
        {
            if (player.position.x > enemy.transform.position.x && Movement.FacingDirection == -1)
                Movement.Flip();
            else if (player.position.x < enemy.transform.position.x && Movement.FacingDirection == 1)
                Movement.Flip();
            if (Mathf.Abs(enemy.transform.position.x - player.position.x) > 2.5f)
                Movement?.SetVelocityX(enemyDataSO.enemyData.moveSpeed * Movement.FacingDirection);
            else
                enemy.isIdle = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemyDataSO.enemyData.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
