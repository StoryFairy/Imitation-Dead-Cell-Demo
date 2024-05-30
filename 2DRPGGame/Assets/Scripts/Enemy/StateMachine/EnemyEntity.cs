using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyEntity : MonoBehaviour
{
    public Core Core { get; private set; }

    protected Movement Movement
    {
        get => movement ?? Core.GetCoreComponent(ref movement);
    }

    protected CollisionSenses CollisionSenses
    {
        get => collisionSenses ?? Core.GetCoreComponent(ref collisionSenses);
    }

    protected Stats Stats
    {
        get => stats ?? Core.GetCoreComponent(ref stats);
    }

    private Movement movement;
    private CollisionSenses collisionSenses;
    protected Stats stats;

    public FiniteStateMachine stateMachine;

    public EnemyDataSO enemyDataSO;
    public Transform attackPosition;
    public bool isDead;
    public bool isIdle;
    public Collider2D combatCollider;

    [HideInInspector] public float lastTimeAttacked;
    public Animator anim { get; private set; }

    public bool isStunned;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        anim = GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentEnemyState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void Die()
    {
        isDead = true;
    }

    public virtual void AnimationFinishTrigger() => stateMachine.CurrentEnemyState.AnimationFinishTrigger();
    public virtual void AttackTrigger() => stateMachine.CurrentEnemyState.AttackTrigger();
    public virtual void DeadTrigger() => stateMachine.CurrentEnemyState.DeadTrigger();

    #region CheckFunctions

    public virtual RaycastHit2D IsPlayerDetected() =>
        Physics2D.Raycast(
            CollisionSenses.WallCheck.position - new Vector3(Movement.FacingDirection * 8, 0, 0),
            Vector2.right * Movement.FacingDirection, 20,
            enemyDataSO.enemyData.whatIsPlayer);

    public bool isPlayerExist() => GameObject.FindWithTag("Player") != null;

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition.position, enemyDataSO.enemyData.attackRadius);
    }
}
