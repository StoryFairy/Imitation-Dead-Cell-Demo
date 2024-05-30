using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WeaponType
{
    Axe_01,
    Axe_02,
    Hummer_01,
    Hummer_02,
    Sword_01,
    Sword_02
}

[CreateAssetMenu(menuName = "Data/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public WeaponType weaponType;
    [field: SerializeField] public Sprite Icon { get; set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
    [field: SerializeField] public int NumberOfAttacks { get; private set; }
    [field: SerializeField] public int criticalHitRate { get; private set; }
    
    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }
    
    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }
    
    public List<Type> GetAllDependencies()
    {
        return ComponentData.Select(component => component.ComponentDependency).ToList();
    }
    
    public void AddData(ComponentData data)
    {
        if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) 
            return;
            
        ComponentData.Add(data);
    }
    
    public string skillScriptName;
    
    public void AddSkillComponent(GameObject weaponObject) {
        var type = Type.GetType(skillScriptName);
        if (type != null) {
            weaponObject.AddComponent(type);
        } else {
            Debug.LogError("Skill script not found: " + skillScriptName);
        }
    }
    
    public void RemoveExistingSkill(GameObject weaponObject) {
        // 假设所有技能脚本都实现了一个共同的接口 ISkill
        var existingSkills = weaponObject.GetComponents<IWeaponSkill>();
        foreach (var skill in existingSkills) {
            Destroy(skill as Component);
        }
    }
}
