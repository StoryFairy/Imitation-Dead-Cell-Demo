using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [Header("Move Details")]
    public float movementVelocity = 10f;

    [Header("Jump Details")]
    public float jumpVelocity = 20f;
    public int amountOfJumps = 2;
    
    [Header("Wall Jump Details")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    public float coyoteTime = 0.2f;

    public float wallSlideVelocity = 2f;
    public float wallClimbVelocity = 3f;

    [Header("Dash Details")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 20f;
    public float dashEndYMultiplier=0.2f;
    public float distBetweenAfterImages = 0.5f;
    public float drag = 10f;

    [Header("Weapon Details")]
    public WeaponDataSO weapon;

    [Header("Attack Details")]
    public float baseAttackMultiplier = 10f;
    
    public float criticalHitRate=0f;
    public float criticalDamage=1.5f;

    [Header("Health Details")]
    public float maxHealth = 10000f;
}
