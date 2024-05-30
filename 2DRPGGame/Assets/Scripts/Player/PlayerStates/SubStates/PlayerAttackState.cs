using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;

    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerDataSO playerDataSO,
        string animBoolName,
        Weapon weapon
    ) : base(player, stateMachine, playerDataSO, animBoolName)
    {
        this.weapon = weapon;

        weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
    
    public bool CanTransitionToAttackState() => weapon.CanEnterAttack;
}
