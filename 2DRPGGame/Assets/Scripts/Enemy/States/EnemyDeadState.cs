using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState<T> : EnemyState where T : EnemyEntity
{
    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    protected T enemy;
    public EnemyDeadState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, T enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
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
        if(enemy.combatCollider!=null)
            enemy.combatCollider.enabled = false;

        GameObject icon=GameObject.Instantiate(enemyDataSO.enemyData.coin, enemy.transform.position, Quaternion.identity);
        icon.GetComponent<ItemData>().value = enemyDataSO.enemyData.coins;
        
        if (GameManager.Instance.RandomNumber() < 15)
        {
            GameObject.Instantiate(enemyDataSO.enemyData.healthPotion, enemy.transform.position, Quaternion.identity);
        }
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

    public override void DeadTrigger()
    {
        base.DeadTrigger();
        
        GameObject.Destroy(enemy.gameObject);
    }
}
