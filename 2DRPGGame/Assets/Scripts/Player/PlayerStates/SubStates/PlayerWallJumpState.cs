using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(player, stateMachine, playerDataSO, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        Movement?.SetVelocity(playerDataSO.playerData.wallJumpVelocity,playerDataSO.playerData.wallJumpAngle,wallJumpDirection);
        Movement?.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        player.Anim.SetFloat("yVelocity",Movement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity",Mathf.Abs(Movement.CurrentVelocity.x));

        if (Time.time >= startTime + playerDataSO.playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = Movement.FacingDirection;
        }
    }
}
