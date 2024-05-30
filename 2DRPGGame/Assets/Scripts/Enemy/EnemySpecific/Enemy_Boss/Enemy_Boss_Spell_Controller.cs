using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Spell_Controller : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private EnemyDataSO enemyDataSO;
    [SerializeField] private DamageReceiver playerDamageable;
    [SerializeField] private Player player;
    [SerializeField] public Animator spellAnim;

    private float totalTime;

    public bool isExit;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerDamageable = player.GetComponentInChildren<DamageReceiver>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        totalTime += Time.deltaTime;
        if (totalTime >= 1f)
        {
            Player player= collider.GetComponent<Player>();

            if (player != null)
            {
                player.isStunned = true;
                if (playerDamageable != null)
                {
                    playerDamageable.Damage(enemyDataSO.enemyData.attackDamage *
                                            enemyDataSO.enemyData.baseAttackMultiplier * 3);
                }

                player.playerUI.UpdateHealth();
            }
            totalTime = 0f;
        }
    }

    private void ExitTrigger() => isExit = true;
}
