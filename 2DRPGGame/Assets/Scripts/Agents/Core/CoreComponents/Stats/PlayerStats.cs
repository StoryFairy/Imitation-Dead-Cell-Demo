using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public Player player;
    protected override void Awake()
    {
        base.Awake();
        Health.MaxValue = player.playerDataSO.playerData.maxHealth;
    }

    private void OnEnable()
    {
        Health.OnCurrentValueZero += PlayerDead;
    }

    private void OnDisable()
    {
        Health.OnCurrentValueZero -= PlayerDead;
    }

    private void PlayerDead()
    {
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.PlayerDeath);
    }
}
