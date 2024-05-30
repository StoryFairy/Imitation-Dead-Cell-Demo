using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Necromancer : EnemyEntity
{
    [SerializeField] public GameObject Fireball;
    [SerializeField] public GameObject FireballImpact;
    
    
    public Enemy_Necromancer_IdleState IdleState { get; private set; }
    public Enemy_Necromancer_MoveState MoveState { get; private set; }
    public Enemy_Necromancer_BattleState BattleState { get; private set; }
    public Enemy_Necromancer_AttackState AttackState { get; private set; }
    public Enemy_Necromancer_StunnedState StunnedState { get; private set; }
    public Enemy_Necromancer_DeadState DeadState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        IdleState = new Enemy_Necromancer_IdleState(this, stateMachine, "Idle", enemyDataSO, this);
        MoveState = new Enemy_Necromancer_MoveState(this, stateMachine, "Move", enemyDataSO, this);
        BattleState = new Enemy_Necromancer_BattleState(this, stateMachine, "Move", enemyDataSO, this);
        AttackState = new Enemy_Necromancer_AttackState(this, stateMachine, "Attack", enemyDataSO, this);
        StunnedState = new Enemy_Necromancer_StunnedState(this, stateMachine, "Stunned", enemyDataSO, this);
        DeadState = new Enemy_Necromancer_DeadState(this, stateMachine, "Dead", enemyDataSO, this);
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
