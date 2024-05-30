using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour,IMMEventListener<CorgiEngineEvent>
{
    private Player Player;
    private PlayerStats playerStats;
    private Weapon weapon;

    public Slider HealthBar;
    public Image WeaponIcon;
    public Text WeaponMsg;
    
    public void UpdateHealth()
    {
        HealthBar.value = playerStats.Health.CurrentValue / playerStats.Health.MaxValue;
    }

    public void UpdateWeapon(WeaponDataSO data)
    {
        WeaponIcon.sprite = data.Icon;
        WeaponMsg.text = data.Name + " : " + data.Description;
    }

    public void OnMMEvent(CorgiEngineEvent eventType)
    {
        if(eventType.EventType==CorgiEngineEventTypes.LevelStart)
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            playerStats = Player.Core.GetCoreComponent<PlayerStats>();
            weapon = Player.GetComponent<Weapon>();
        }
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
