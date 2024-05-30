using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CastleGameManager : SceneGameManager,IMMEventListener<MMGameEvent>
{
    public GameObject dialogueManager;
    
    public void Awake()
    {
        dialogueManager.SetActive(false);
    }

    private void Start()
    {
        if (GameManager.Instance.gameDataSO != null)
        {
            CorgiEngineEvent.Trigger(CorgiEngineEventTypes.LevelStart);
            if (!GameManager.Instance.gameDataSO.GameData.Story_00)
            {
                MMGameEvent.Trigger("Dialogue");
                GameManager.Instance.gameDataSO.GameData.Story_00 = true;
            }
            player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.playerDataSO.playerData = GameManager.Instance.gameDataSO.GameData.playerData;
            player.GetComponentInChildren<Weapon>().weaponData = null;
            player.playerDataSO.playerData.weapon = null;
        }
        camera.Follow = player.transform;
        coinsSlot=GameObject.Find("ItemSlot").GetComponent<UI_ItemSlot>();
        coinsSlot.stackSize=GameManager.Instance.gameDataSO.GameData.coins;
        coinsSlot.UpdateSlot();
    }

    public void OnMMEvent(MMGameEvent eventType)
    {
        if (eventType.EventName == "Dialogue")
        {
            dialogueManager.SetActive(true);
            GameManager.Instance.Pause(PauseMethods.NoPauseMenu);
            DialogueManager.Instance.InitCurrentDialogue(0);
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening<MMGameEvent>();
    }
    
    private void OnDisable()
    {
        this.MMEventStopListening<MMGameEvent>();
    }
}
