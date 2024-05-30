using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    public event Action OnSkill;
    public event Action OffSkill;
    
    [SerializeField] private Weapon weapon;

    private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();

    private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();

    private List<Type> componentDependencies = new List<Type>();

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        GenerateWeapon(weapon.weaponData);
    }

    public void GenerateWeapon(WeaponDataSO data)
    {
        
        weapon.SetData(data);

        if (data is null)
        {
            weapon.SetCanEnterAttack(false);
            return;
        }

        componentAlreadyOnWeapon.Clear();
        componentsAddedToWeapon.Clear();
        componentDependencies.Clear();

        componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

        componentDependencies = data.GetAllDependencies();

        foreach (var dependency in componentDependencies)
        {
            if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                continue;

            var weaponComponent =
                componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

            if (weaponComponent == null)
            {
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
            }

            weaponComponent.Init();

            componentsAddedToWeapon.Add(weaponComponent);
        }

        var componentsToRemove = componentAlreadyOnWeapon.Except(componentsAddedToWeapon);

        foreach (var weaponComponent in componentsToRemove)
        {
            Destroy(weaponComponent);
        }

        anim.runtimeAnimatorController = data.AnimatorController;
        weapon.playerData.playerData.criticalHitRate = data.criticalHitRate;
        OffSkill?.Invoke();
        data.RemoveExistingSkill(this.gameObject);
        
        
        data.AddSkillComponent(this.gameObject);
        OnSkill?.Invoke();
        
        weapon.SetCanEnterAttack(true);
    }
}
