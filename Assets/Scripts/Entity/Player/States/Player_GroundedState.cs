using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName) : base(playerBrain, stateMachine,  animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(playerBrain.playerInputActions.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(playerBrain.primaryAttackState);
        }
        if(playerBrain.playerInputActions.Player.CounterAttack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(playerBrain.counterAttackState);
        }
        // Temporary testing: Press L to transform
        if (Input.GetKeyDown(KeyCode.L))
        {
            stateMachine.ChangeState(playerBrain.transformationState);
        }

    }
}   