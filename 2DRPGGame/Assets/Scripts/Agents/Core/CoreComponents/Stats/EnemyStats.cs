using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public EnemyEntity enemy;
    protected override void Awake()
    {
        base.Awake();
        Health.MaxValue = enemy.enemyDataSO.enemyData.maxHealth;
    }

    private void OnEnable()
    {
        Health.OnCurrentValueZero += EnemyDead;
        Health.OnCurrentValueZero += enemy.Die;
    }

    private void OnDisable()
    {
        Health.OnCurrentValueZero -= EnemyDead;
        Health.OnCurrentValueZero -= enemy.Die;
    }

    private void EnemyDead()
    {
        //掉落物品
    }
}
