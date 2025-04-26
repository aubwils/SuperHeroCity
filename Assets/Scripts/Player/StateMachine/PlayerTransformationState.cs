using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationState : PlayerState
{

    public PlayerTransformationState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.SetCanMove(false);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        player.playerMovement.SetCanMove(true);
    }
}
