using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    private Vector2 movementInput;
    private Vector2 lastMovementDirection; // Store the last non-zero movement direction

    public Player_MoveState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName) : base(playerBrain, stateMachine,  animBoolName)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        // Update animator direction here
        playerBrain.playerMovement.UpdateAnimatorMovementDirection();

        // Get movement input from PlayerMovement
        Vector2 movementInput = playerBrain.playerMovement.GetMovementInput();

        // Transition to idle state if no input
        if (movementInput == Vector2.zero)
        {
            stateMachine.ChangeState(playerBrain.idleState);
        }
    }    

    public override void Exit()
    {
        base.Exit();
    }
}
