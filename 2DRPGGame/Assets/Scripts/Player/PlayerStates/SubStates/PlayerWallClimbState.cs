using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(player, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(!isExitingState)
        {
            Movement?.SetVelocityY(playerDataSO.playerData.wallClimbVelocity);

            if (yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
