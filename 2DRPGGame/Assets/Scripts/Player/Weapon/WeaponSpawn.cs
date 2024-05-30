using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponSpawn : MonoBehaviour
{
    public WeaponDataSO[] weaponDatas;
    public WeaponDataSO currentWeapon { get; private set; }

    private SpriteRenderer icon;

    private void Awake()
    {
        Initialization();
        icon = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        icon.sprite = currentWeapon.Icon;
    }

    private void Initialization()
    {
        currentWeapon=weaponDatas[Random.Range(0, weaponDatas.Length)];
    }
}
