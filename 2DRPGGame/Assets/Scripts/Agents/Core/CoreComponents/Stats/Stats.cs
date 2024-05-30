using System;
using UnityEngine;

public class Stats : CoreComponent
{
    [field: SerializeField]  public Stat Health { get; private set; }
    protected void Start()
    {
        base.Awake();
        Health.Init();
    }
}
