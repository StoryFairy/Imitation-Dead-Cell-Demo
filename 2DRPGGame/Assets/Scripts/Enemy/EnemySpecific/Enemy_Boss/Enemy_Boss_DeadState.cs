using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_DeadState : EnemyDeadState<Enemy_Boss>
{
    public Enemy_Boss_DeadState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Boss enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isDead = false;
        enemy.bossUI.gameObject.SetActive(false);
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
        if (!GameManager.Instance.gameDataSO.GameData.Story_01)
        {
            MMGameEvent.Trigger("Dialogue");
            GameManager.Instance.gameDataSO.GameData.Story_01 = true;
        }
        base.DeadTrigger();
    }
}
