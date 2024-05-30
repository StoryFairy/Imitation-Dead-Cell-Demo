using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO,
        string animBoolName) : base(player, stateMachine, playerDataSO, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(!isExitingState)
        {
            Movement?.SetVelocityY(-playerDataSO.playerData.wallSlideVelocity);

            if (grabInput && yInput == 0)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
