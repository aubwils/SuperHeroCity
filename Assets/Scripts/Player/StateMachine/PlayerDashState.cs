using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState

{
    private float dashTimer; // Tracks how long the dash lasts
    private Vector2 dashDirection; // Direction of the dash

    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) 
        : base(stateMachine, player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        // Set the dash direction based on the last movement direction
        dashDirection = player.playerMovement.GetLastMovementDirection();
        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.down; // Default to down if no direction
        }
        dashTimer = player.playerMovement.dashDuration; // Initialize the dash timer
        Debug.Log("Player started dashing");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer > 0f)
        {
            // Move the player in the dash direction
            player.transform.Translate(dashDirection * player.playerMovement.dashSpeed * Time.fixedDeltaTime);
            dashTimer -= Time.fixedDeltaTime;
        }
        else
        {
            // Transition back to idle state after the dash
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Player finished dashing");
    }

}
