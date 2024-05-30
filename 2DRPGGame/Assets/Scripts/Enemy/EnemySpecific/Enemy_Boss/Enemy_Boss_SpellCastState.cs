using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_SpellCastState : EnemyState
{
    private Enemy_Boss enemy;
    public Enemy_Boss_SpellCastState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName, EnemyDataSO enemyDataSO, Enemy_Boss enemy) : base(entity, stateMachine, animBoolName, enemyDataSO)
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

        stateTimer = 5;

        enemy.Spell.transform.position = new Vector2(GameObject.FindWithTag("Player").transform.position.x, 0);
        enemy.Spell.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Spell.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stateTimer < 0)
        {
            enemy.SpellController.spellAnim.SetBool("Exit", true);
            if (enemy.SpellController.isExit)
                stateMachine.ChangeState(enemy.TeleportState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
