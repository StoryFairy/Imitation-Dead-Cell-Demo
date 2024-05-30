using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSet : InteractableBase
{
    [SerializeField] private WeaponSpawn weapon;
    
    public override void BeginInteract()
    {
        base.BeginInteract();
    }
    
    public override void Interact()
    {
        base.Interact();
        if (player.InputHandler.InteractInput)
        {
            player.playerUI.UpdateWeapon(weapon.currentWeapon);
            player.GetComponentInChildren<Weapon>().SetData(weapon.currentWeapon);
            player.GetComponentInChildren<WeaponGenerator>().GenerateWeapon(weapon.currentWeapon);
            player.playerDataSO.playerData.weapon = weapon.currentWeapon;
            Destroy(this.gameObject);
        }
    }
    
    public override void EndInteract()
    {
        base.EndInteract();
    }
    
    public override bool IsInteractionAllowed()
    {
        return true;
    }
}
