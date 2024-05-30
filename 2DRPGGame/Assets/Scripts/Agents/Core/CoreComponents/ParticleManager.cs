using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : CoreComponent
{
    private SpriteRenderer sr;
    [Header("Flash FX")] [SerializeField] private Material hitMat;
    private Material originalMat;
    
    private Movement movement;
    private Transform particleContainer;

    protected override void Awake()
    {
        base.Awake();
        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }

    private void Start()
    {
        movement = core.GetCoreComponent<Movement>();
        sr= GetComponentInParent<SpriteRenderer>();
        originalMat = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originalMat;
    }

    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation, particleContainer);
    }

    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }
    
    public GameObject StartParticles(GameObject particlePrefab,int facDirection)
    {
        Quaternion rotation = Quaternion.identity;
        if (facDirection == -1)
        {
            // 如果方向为-1，则进行180度旋转
            rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        return StartParticles(particlePrefab, transform.position, rotation);
    }

    public GameObject StartWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }

    public GameObject StartWithRandomRotation(GameObject prefab, Vector2 offset)
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(prefab, FindRelativePoint(offset), randomRotation);
    }
    
    public GameObject StartParticlesRelative(GameObject particlePrefab, Vector2 offset, Quaternion rotation)
    {
        var pos = FindRelativePoint(offset);

        return StartParticles(particlePrefab, pos, rotation);
    }

    private Vector2 FindRelativePoint(Vector2 offset) => movement.FindRelativePoint(offset);
}
