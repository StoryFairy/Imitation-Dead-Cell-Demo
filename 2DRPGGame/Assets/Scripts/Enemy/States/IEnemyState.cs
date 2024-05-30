using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void DoChecks();
    void Enter();
    void Exit();
    void LogicUpdate();
    void PhysicsUpdate();
}
