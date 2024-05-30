using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGood : InteractableBase
{
    public int price;
    [SerializeField] private Text priceText;
    public GameObject good;

    private void Awake()
    {
        priceText.text = "$"+price.ToString();
    }

    public override void BeginInteract()
    {
        base.BeginInteract();
    }

    public override void Interact()
    {
        base.Interact();
        
        if (player.InputHandler.InteractInput)
        {
            if (Inventory.Instance.itemSlots.stackSize < price)
                return;
            Inventory.Instance.RemoveItem(price);
            Instantiate(good, transform.position, Quaternion.identity);
            
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
