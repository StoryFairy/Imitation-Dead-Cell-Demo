using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HealthPotion,
    Coin
}

public class ItemData : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int value;
    [SerializeField] private ItemsMusic music;
    
    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemIcon;
        gameObject.name = "Item Object_" + itemName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        UI_ItemSlot itemSlots = GameObject.Find("ItemSlot").GetComponent<UI_ItemSlot>();
        if (player != null)
        {
            if (itemType == ItemType.Coin)
            {
                Inventory.Instance.AddItem(value);
            }
            else if (itemType == ItemType.HealthPotion)
                player.GetComponentInChildren<PlayerStats>().Health.Increase(value);
            music.PlayMusic();
            Destroy(gameObject);
        }
    }
}
