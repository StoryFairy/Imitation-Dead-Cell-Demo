using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using Edgar.Unity;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ForestGameManager : SceneGameManager,IMMEventListener<CorgiEngineEvent>
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
