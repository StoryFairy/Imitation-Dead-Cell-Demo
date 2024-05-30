using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public EnemyState CurrentEnemyState { get; private set; }

    public void Initialize(EnemyState startingEnemyState)
    {
        CurrentEnemyState = startingEnemyState;
        CurrentEnemyState.Enter();
    }

    public void ChangeState(EnemyState newEnemyState)
    {
        CurrentEnemyState.Exit();
        CurrentEnemyState = newEnemyState;
        CurrentEnemyState.Enter();
    }
}
