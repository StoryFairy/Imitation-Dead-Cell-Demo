using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO, string animBoolName) : base(player, stateMachine, playerDataSO, animBoolName)
    {
        amountOfJumpsLeft = playerDataSO.playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        Movement?.SetVelocityY(playerDataSO.playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
    }

    public bool CanJump()
    {
        return  amountOfJumpsLeft > 0;
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerDataSO.playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
