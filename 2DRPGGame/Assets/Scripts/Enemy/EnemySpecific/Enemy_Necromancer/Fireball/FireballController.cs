using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool worked;
    [SerializeField] private EnemyDataSO enemy;
    [SerializeField] private ParticleSystem impactParticles; 
    
    private Transform player;
    private Vector2 velocity;
    private string targetLayerName = "Player";
    
    private void Start()
    {
        player=GameObject.FindWithTag("Player").transform;
        
        velocity = player.position - transform.position;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            collision.GetComponent<IDamageable>()
                .Damage(enemy.enemyData.attackDamage * enemy.enemyData.baseAttackMultiplier);
            player.gameObject.GetComponent<Player>().playerUI.UpdateHealth();
        }
        Instantiate(impactParticles, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
