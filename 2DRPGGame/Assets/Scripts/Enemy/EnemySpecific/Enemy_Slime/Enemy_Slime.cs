using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SlimeType
{
    big,
    medium,
    small
}

public class Enemy_Slime : EnemyEntity
{
    [SerializeField] public SlimeType slimeType;
    [SerializeField] public int slimesToCreate;
    [SerializeField] public GameObject slimePrefab;
    [SerializeField] public Material slimeMaterial;

    public Enemy_Slime_IdleState IdleState { get; private set; }
    public Enemy_Slime_MoveState MoveState { get; private set; }
    public Enemy_Slime_BattleState BattleState { get; private set; }
    public Enemy_Slime_AttackState AttackState { get; private set; }
    public Enemy_Slime_StunnedState StunnedState { get; private set; }
    public Enemy_Slime_DeadState DeadState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        IdleState = new Enemy_Slime_IdleState(this, stateMachine, "Idle", enemyDataSO, this);
        MoveState = new Enemy_Slime_MoveState(this, stateMachine, "Move", enemyDataSO, this);
        BattleState = new Enemy_Slime_BattleState(this, stateMachine, "Move", enemyDataSO, this);
        AttackState = new Enemy_Slime_AttackState(this, stateMachine, "Attack", enemyDataSO, this);
        StunnedState = new Enemy_Slime_StunnedState(this, stateMachine, "Stunned", enemyDataSO, this);
        DeadState = new Enemy_Slime_DeadState(this, stateMachine, "Dead", enemyDataSO, this);
    }

    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    public override void Update()
    {
        base.Update();
        if (isStunned)
        {
            stateMachine.ChangeState(StunnedState);
            isStunned = false;
        }
    }
}