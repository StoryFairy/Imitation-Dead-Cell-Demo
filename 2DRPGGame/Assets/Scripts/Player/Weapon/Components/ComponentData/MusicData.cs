using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicData : ComponentData<AttackMusic>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponMusic);
    }
}
