using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPool : MMSingleton<SelectionPool>
{
    public GameObject selectionPrefab;
    public int selectionCount;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    private List<GameObject> tempObjects = new List<GameObject>();
    
    protected override void Awake()
    {
        base.Awake();
        FillPool();
    }

    public void FillPool()
    { 
        Vector3 spawnPos = new Vector3(-40f, 0f, 25f);
        for (int i = 0; i < selectionCount; i++)
        {
            var newSelection = Instantiate(selectionPrefab);
            newSelection.transform.SetParent(transform);

            newSelection.transform.position = spawnPos;
            newSelection.transform.localScale = new Vector3(1, 1, 1);

            spawnPos.y += 2f;
            
            ReturnPool(newSelection);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }

    public void ReturnAllPool()
    {
        foreach (var box in tempObjects)
        {
            box.SetActive(false);
            availableObjects.Enqueue(box);
        }
    }

    public GameObject GetFormPool()
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }

        var outBox = availableObjects.Dequeue();

        outBox.SetActive(true);
        tempObjects.Add(outBox);
        
        return outBox;
    }
}
