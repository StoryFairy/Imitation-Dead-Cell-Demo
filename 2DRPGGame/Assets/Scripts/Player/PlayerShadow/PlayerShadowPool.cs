using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowPool : MMSingleton<PlayerShadowPool>
{
    public GameObject shadowPrefab;

    public int shadowCount;
    private Queue<GameObject> availableobjects = new Queue<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);

            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableobjects.Enqueue(gameObject);
    }

    public GameObject GetFormPool()
    {
        if (availableobjects.Count == 0)
        {
            FillPool();
        }

        var outShadow = availableobjects.Dequeue();

        outShadow.SetActive(true);

        return outShadow;
    }

}
