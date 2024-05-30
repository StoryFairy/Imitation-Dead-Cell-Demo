using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MMSingleton<Inventory>
{
    public UI_ItemSlot itemSlots;
    
    public void AddItem(int coins)
    {
        itemSlots.stackSize += coins;
        itemSlots.UpdateSlot();
    }
    
    public void RemoveItem(int coins)
    {
        itemSlots.stackSize -= coins;
        itemSlots.UpdateSlot();
    }
}
