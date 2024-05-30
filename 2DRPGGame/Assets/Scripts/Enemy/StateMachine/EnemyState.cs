using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : IEnemyState
{
    protected FiniteStateMachine stateMachine;
    protected EnemyEntity EnemyEntity;
    protected EnemyDataSO enemyDataSO;
    protected Core core;

    public string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public EnemyState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO)
    {
        this.EnemyEntity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.enemyDataSO = enemyDataSO;
        core = EnemyEntity.Core;
    }
    
    public virtual void DoChecks()
    {

    }
    
    public virtual void Enter()
    {
        triggerCalled = false;
        EnemyEntity.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        EnemyEntity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

    public virtual void AttackTrigger()
    {
    }

    public virtual void DeadTrigger()
    {
    }
}
