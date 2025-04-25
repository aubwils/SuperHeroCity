using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector2 movementInput;
    private Vector2 lastMovementDirection; // Store the last non-zero movement direction

    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        player.playerMovement.UpdateAnimatorMovementDirection();

        // Get movement input from PlayerMovement
        Vector2 movementInput = player.playerMovement.GetMovementInput();

        // Transition to idle state if no input
        if (movementInput == Vector2.zero)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }    

    public override void Exit()
    {
        base.Exit();
    }
}
