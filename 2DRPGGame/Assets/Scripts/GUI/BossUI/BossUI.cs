using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;

    public Slider HealthBar;

    public void UpdateHealth()
    {
        HealthBar.value = enemyStats.Health.CurrentValue / enemyStats.Health.MaxValue;
    }
}
