using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationState : PlayerState
{

    public PlayerTransformationState(Player player, StateMachine stateMachine,  string animBoolName) : base(player, stateMachine,  animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerMovement.SetCanMove(false);
        //Debug.Log("Transformation State Enter");
    }

    public override void Update()
    {
        base.Update();
        if(animationTriggerCalled)
        {
            player.ToggleHeroIdentity();
            player.playerMovement.SetCanMove(true);
            //Debug.Log("Tigger Called");
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Transformation State Exit");

    }
}
