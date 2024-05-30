using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KnockBackData : ComponentData<AttackKnockBack>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponKnockBack);
    }
}
