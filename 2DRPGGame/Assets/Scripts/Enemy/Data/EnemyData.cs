using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    [Header("Stats")]
    public int maxHealth;
    
    [Header("Move")]
    public float idleTime;
    public float moveSpeed;
    
    [Header("Attack")]
    public float attackDistance;
    public float attackCooldown;
    public float attackRadius;
    
    public LayerMask whatIsPlayer;
    
    public float attackDamage;
    public float baseAttackMultiplier;

    [Header("Stun")]
    public float stunDuration;

    [Header("DropItems")] 
    public int coins;
    public GameObject coin;
    public GameObject healthPotion;
}
