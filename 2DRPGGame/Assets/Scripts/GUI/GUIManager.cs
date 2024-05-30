using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MMSingleton<GUIManager>, IMMEventListener<MMGameEvent>
{
    public GameObject PauseScreen;

    public virtual void SetPause(bool state)
    {
        if (PauseScreen != null)
        {
            PauseScreen.SetActive(state);
        }
    }
    
    public void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName == "GameOver")
            Debug.Log("GUIManager----OnMMEvent");
    }

    protected virtual void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
    }

    protected virtual void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
    }
}


