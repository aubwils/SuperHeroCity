using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        player.playerMovement.UpdateAnimatorMovementDirection();

        // Get movement input from PlayerMovement
        Vector2 movementInput = player.playerMovement.GetMovementInput();

        // Transition to move state if input is detected
        if (movementInput != Vector2.zero)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Idle State Exit");
    }


}
