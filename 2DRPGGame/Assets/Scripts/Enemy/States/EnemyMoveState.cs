using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState<T> : EnemyState where T : EnemyEntity
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

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayer;
    
    protected T enemy;
    public EnemyMoveState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, T enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
    {
        this.enemy = enemy;
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        if (CollisionSenses)
        {
            isDetectingLedge = CollisionSenses.LedgeVertical;
            isDetectingWall = CollisionSenses.WallFront;
        }

        isPlayer = EnemyEntity.IsPlayerDetected();
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
        Movement?.SetVelocityX(enemyDataSO.enemyData.moveSpeed * Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
