using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using Edgar.Unity;
using UnityEngine;

public class UnderGroundGameManager : SceneGameManager,IMMEventListener<CorgiEngineEvent>
{
    public override void OnMMEvent(CorgiEngineEvent eventType)
    {
        base.OnMMEvent(eventType);
        ExitDoor[] edDoors = GameObject.FindObjectsOfType<ExitDoor>();
        edDoors[0].type = DoorName.Forest;
        edDoors[1].type = DoorName.Sea;
    }

    private void OnEnable()
    {
        this.MMEventStartListening<CorgiEngineEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<CorgiEngineEvent>();
    }

}
