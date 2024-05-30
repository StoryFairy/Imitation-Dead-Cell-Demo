using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorName
{
    Castle,
    Underground,
    Forest,
    Sea,
    CastleVania,
}

public class ExitDoor : InteractableBase
{
    public DoorName type;
    public Dictionary<DoorName, string> doorNames = new Dictionary<DoorName, string>
    {
        { DoorName.Castle, "00-Castle" },
        { DoorName.Underground, "01-Underground" },
        { DoorName.Forest, "02-Forest" },
        { DoorName.Sea, "02-Sea" },
        { DoorName.CastleVania, "03-CastleVania" }
    };
    
    public override void BeginInteract()
    {
        base.BeginInteract();
    }

    public override void Interact()
    {
        base.Interact();
        if (player.InputHandler.InteractInput)
        {
            if(doorNames.TryGetValue(type, out string name))
            {
                LevelManager.Instance.GotoLevel(name);
            }
            
        }
    }

    public override void EndInteract()
    {
        base.EndInteract();
    }

}
