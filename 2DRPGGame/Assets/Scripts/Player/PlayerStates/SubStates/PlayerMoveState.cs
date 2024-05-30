using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(player, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.CheckIfShouldFlip(xInput);

        Movement?.SetVelocityX(playerDataSO.playerData.movementVelocity * xInput);

        if (xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        PlayerShadowPool.Instance.GetFormPool();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
