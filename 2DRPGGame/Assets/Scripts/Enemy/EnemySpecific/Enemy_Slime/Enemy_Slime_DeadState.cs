using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Slime_DeadState : EnemyDeadState<Enemy_Slime>
{
    private bool isSpawn = true;
    private SlimeType slimeType;
    private Vector3 newScale;
    private float minoffset = -2f;
    private float maxoffset = 2f;

    public Enemy_Slime_DeadState(EnemyEntity entity, FiniteStateMachine stateMachine, string animBoolName,
        EnemyDataSO enemyDataSO, Enemy_Slime enemy) : base(entity, stateMachine, animBoolName, enemyDataSO, enemy)
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
        slimeType = enemy.slimeType;
        InitNewSlime();
        CreateSlimes(enemy.slimesToCreate, newScale);
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

    private void InitNewSlime()
    {
        switch (slimeType)
        {
            case SlimeType.big:
                enemy.slimesToCreate = 2;
                newScale = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            case SlimeType.medium:
                enemy.slimesToCreate = 2;
                newScale = new Vector3(0.8f, 0.8f, 0.8f);
                break;
            case SlimeType.small:
                isSpawn = false;
                break;
        }
    }

    public void CreateSlimes(int amountOfSlimes, Vector3 newScale)
    {
        if (isSpawn)
        {
            for (int i = 0; i < amountOfSlimes; i++)
            {
                Vector3 offset = new Vector3(Random.Range(minoffset, maxoffset), 0f, 0);
                GameObject newSlime =
                    GameObject.Instantiate(enemy.slimePrefab, enemy.transform.position + offset, Quaternion.identity);
                newSlime.transform.localScale = newScale;
                newSlime.GetComponent<EnemyEntity>().combatCollider.enabled = true;
                newSlime.GetComponent<Enemy_Slime>().slimeType = slimeType + 1;
                newSlime.GetComponent<SpriteRenderer>().material = enemy.slimeMaterial;
            }
        }
    }
}
