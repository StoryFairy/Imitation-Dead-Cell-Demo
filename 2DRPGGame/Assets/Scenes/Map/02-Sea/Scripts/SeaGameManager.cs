using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SeaGameManager : SceneGameManager,IMMEventListener<CorgiEngineEvent>
{
    public override void OnMMEvent(CorgiEngineEvent eventType)
    {
        base.OnMMEvent(eventType);
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
