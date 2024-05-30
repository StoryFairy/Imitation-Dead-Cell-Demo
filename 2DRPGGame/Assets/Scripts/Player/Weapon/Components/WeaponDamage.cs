using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : WeaponComponent<DamageData, AttackDamage>
{
    public Core core;
    protected PlayerStats Stats
    {
        get => stats ?? weapon.Core.GetCoreComponent(ref stats);
    }
    private PlayerStats stats;
    private EnemyEntity enemy;
    
    private WeaponActionHitBox hitBox;

    private void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IDamageable damageable))
            {
                item.GetComponentInParent<EnemyEntity>().isStunned = true;
                float currentDamage = 0;

                if (GameManager.Instance.RandomNumber() <= weapon.playerData.playerData.criticalHitRate) //暴击
                {
                    currentDamage = currentAttackData.Amount * weapon.playerData.playerData.baseAttackMultiplier *
                                    weapon.playerData.playerData.criticalDamage;
                }
                else //未暴击
                {
                    currentDamage = currentAttackData.Amount * weapon.playerData.playerData.baseAttackMultiplier;
                }

                switch (weapon.weaponData.weaponType)
                {
                    case WeaponType.Axe_01:
                        enemy = item.GetComponentInParent<EnemyEntity>();
                        StartCoroutine(FreezeEnemy(item));
                        break;
                    case WeaponType.Axe_02:
                        Stats.Health.Increase(currentAttackData.Amount *
                                                    weapon.playerData.playerData
                                                        .baseAttackMultiplier * 0.1f);
                        break;
                    case WeaponType.Hummer_01:
                        weapon.anim.speed += 0.05f;
                        break;
                    case WeaponType.Hummer_02:
                        currentDamage = currentDamage * 1.3f;
                        break;
                    case WeaponType.Sword_01:
                        if (GameManager.Instance.RandomNumber() <= 30)
                            currentDamage = currentDamage * 2;
                        break;
                    case WeaponType.Sword_02:
                        item.GetComponentInChildren<DamageReceiver>().continued = true;
                        break;
                }
                
                damageable.Damage(currentDamage);
                item.GetComponentInParent<Enemy_Boss>()?.bossUI.UpdateHealth();
            }
        }
    }

    protected override void Start()
    {
        base.Start();

        hitBox = GetComponent<WeaponActionHitBox>();

        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }

    IEnumerator FreezeEnemy(Collider2D item)
    {
        enemy.anim.speed = 0.6f;
        yield return new WaitForSeconds(4f);
        if(enemy!=null)
            enemy.anim.speed = 1f;
    }
}
