using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TransformationState : PlayerState
{

    public Player_TransformationState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName) : base(playerBrain, stateMachine,  animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerBrain.playerMovement.SetCanMove(false);
        //Debug.Log("Transformation State Enter");
    }

    public override void Update()
    {
        base.Update();
        if(animationTriggerCalled)
        {
            playerBrain.ToggleHeroIdentity();
            playerBrain.playerMovement.SetCanMove(true);
            //Debug.Log("Tigger Called");
            stateMachine.ChangeState(playerBrain.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Transformation State Exit");

    }
}
