using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedState : PlayerState
{
    public PlayerStunnedMusic PlayerStunnedMusic;
    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;
    
    public PlayerStunnedState(Player player, PlayerStateMachine stateMachine, PlayerDataSO playerDataSO,
        string animBoolName) : base(player, stateMachine, playerDataSO, animBoolName)
    {
        PlayerStunnedMusic = player.GetComponent<PlayerStunnedMusic>();
    }

    public override void Enter()
    {
        base.Enter();
        PlayerStunnedMusic.PlayMusic();
        Movement.SetVelocity(4, new Vector2(1, 1) * -Movement.FacingDirection, 3);
    }

    public override void Exit()
    {
        base.Exit();
        player.isStunned = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
