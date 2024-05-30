using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemText;
    public int stackSize;

    public void UpdateSlot()
    {
        itemText.text = stackSize.ToString();
        GameManager.Instance.gameDataSO.GameData.coins = stackSize;
    }
}
