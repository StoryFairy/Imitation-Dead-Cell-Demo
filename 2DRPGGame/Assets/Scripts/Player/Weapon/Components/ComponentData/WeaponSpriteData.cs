using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteData : ComponentData<AttackSprites>
{
    [field: SerializeField] public int attackPause{get; private set;}
    [field: SerializeField] public float attackShakeTime{get; private set;}
    [field: SerializeField] public float attackShakeStrength{get; private set;}
    
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponSprite);
    }
}
