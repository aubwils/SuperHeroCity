using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName) : base(playerBrain, stateMachine,  animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Idle State Enter");
    }

    public override void Update()
    {
        base.Update();
        // Update animator direction here
        playerBrain.playerMovement.UpdateAnimatorMovementDirection();

        // Get movement input from PlayerMovement
        Vector2 movementInput = playerBrain.playerMovement.GetMovementInput();

        // Transition to move state if input is detected
        if (movementInput != Vector2.zero)
        {
            stateMachine.ChangeState(playerBrain.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Idle State Exit");
    }


}
