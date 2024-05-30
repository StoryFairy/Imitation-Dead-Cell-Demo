using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : WeaponComponent<WeaponSpriteData,AttackSprites>
{
    private SpriteRenderer baseSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;

    private int currentWeaponSpriteIndex;

    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }
    
    private void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }

        var currentAttackSprite = currentAttackData.Sprites;
        
        if (currentWeaponSpriteIndex >= currentAttackSprite.Length)
        {
            return;
        }

        if (currentWeaponSpriteIndex == (int)currentAttackSprite.Length / 2)
        {
            AttackSense.Instance.HitPause(data.attackPause);
            AttackSense.Instance.CameraShake(data.attackShakeTime, data.attackShakeStrength);
        }
        
        weaponSpriteRenderer.sprite = currentAttackSprite[currentWeaponSpriteIndex];

        currentWeaponSpriteIndex++;
    }

    protected override void Start()
    {
        base.Start();
        
        baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        
        data = weapon.weaponData.GetData<WeaponSpriteData>();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
    }
}
