using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TreasureType
{
    Coin,
    HealthPotion,
    WeaponSet
}

[Serializable]
public class SerializableKeyValuePair
{
    public TreasureType key;
    public GameObject value;
}


[Serializable]
public class TreasureChest : InteractableBase
{
    public List<SerializableKeyValuePair> keyValuePairs = new List<SerializableKeyValuePair>();


    public override void BeginInteract()
    {
        base.BeginInteract();
    }

    public override void Interact()
    {
        base.Interact();
        if (player.InputHandler.InteractInput)
        {
            GameObject icon=Instantiate(keyValuePairs[Random.Range(0, keyValuePairs.Count)].value, transform.position,
                Quaternion.identity);
            if (icon.GetComponent<ItemData>() != null)
                icon.GetComponent<ItemData>().value = 40;
            Destroy(this.gameObject);
        }
    }

    public override void EndInteract()
    {
        base.EndInteract();
    }

    public override bool IsInteractionAllowed()
    {
        return base.IsInteractionAllowed();
    }
}
