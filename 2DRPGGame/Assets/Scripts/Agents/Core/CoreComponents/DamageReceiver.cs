using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    public bool continued;
    [SerializeField] private GameObject damageParticles;

    private Stats stats;
    private ParticleManager particleManager;
    private Player player;

    public void Damage(float amount)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        particleManager.StartParticles(damageParticles, player.MoveState.Movement.FacingDirection);
        particleManager.StartCoroutine("FlashFX");
        stats.Health.Decrease(amount);

        if (continued)
        {
            StartCoroutine(ContinuedDamage());
            continued = false;
        }
    }

    IEnumerator ContinuedDamage()
    {
        for (int i = 0; i < 6; i++)
        {
            stats.Health.Decrease(5f);
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        stats = core.GetCoreComponent<Stats>();
        particleManager = core.GetCoreComponent<ParticleManager>();
    }
}
