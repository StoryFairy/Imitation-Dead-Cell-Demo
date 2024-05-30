using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SceneGameManager : MonoBehaviour,IMMEventListener<CorgiEngineEvent>
{
    public static readonly string StaticEnvironmentLayer = "StaticEnvironment";
    public new CinemachineVirtualCamera camera;
    public Player player;
    public UI_ItemSlot coinsSlot;

    public GameObject DeadUI;
    public float OutroFadeDuration = 1f;
    public int FaderID = 1;
    public MMTweenType FadeTween = new MMTweenType(MMTween.MMTweenCurve.EaseInOutCubic);
    
    public virtual void OnMMEvent(CorgiEngineEvent eventType)
    {
        if (eventType.EventType == CorgiEngineEventTypes.LevelStart)
        {
            player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.playerDataSO.playerData = GameManager.Instance.gameDataSO.GameData.playerData;
            GameObject.Find("PlayerUI").GetComponent<PlayerUI>().UpdateWeapon(player.playerDataSO.playerData.weapon);
            Inventory.Instance.AddItem(GameManager.Instance.gameDataSO.GameData.coins);
            
            camera.Follow = player.transform;
        }

        if (eventType.EventType == CorgiEngineEventTypes.GameOver)
        {
            DeadUI.SetActive(true);
            MMFadeInEvent.Trigger(OutroFadeDuration, FadeTween, FaderID, true, Vector3.zero);
        }
    }
}
