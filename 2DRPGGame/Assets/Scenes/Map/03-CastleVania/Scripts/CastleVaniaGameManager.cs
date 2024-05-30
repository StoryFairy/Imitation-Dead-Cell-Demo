using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CastleVaniaGameManager : SceneGameManager,IMMEventListener<CorgiEngineEvent>,IMMEventListener<MMGameEvent>
{
    public GameObject dialogueManager;
    
    public void Awake()
    {
        dialogueManager.SetActive(false);
    }
    
    
    
    public override void OnMMEvent(CorgiEngineEvent eventType)
    {
        base.OnMMEvent(eventType);
    }
    
    public void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName == "Dialogue")
        {
            dialogueManager.SetActive(true);
            GameManager.Instance.Pause(PauseMethods.NoPauseMenu);
            DialogueManager.Instance.InitCurrentDialogue(1);
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<CorgiEngineEvent>();
        this.MMEventStartListening<MMGameEvent>();
    }
    
    private void OnDisable()
    {
        this.MMEventStopListening<CorgiEngineEvent>();
        this.MMEventStopListening<MMGameEvent>();
    }


}
