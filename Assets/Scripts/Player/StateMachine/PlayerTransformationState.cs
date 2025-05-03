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
        Debug.Log("Transformation State Enter");
    }

    public override void Update()
    {
        base.Update();
        if(animationTriggerCalled)
        {
            player.ToggleHeroIdentity();
            player.playerMovement.SetCanMove(true);
            Debug.Log("Tigger Called");
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Transformation State Exit");

    }
}
