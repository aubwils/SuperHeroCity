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
            Vector2 moveAmount = dashDirection * player.playerMovement.dashSpeed * Time.fixedDeltaTime;
            Vector2 targetPosition = (Vector2)player.transform.position + moveAmount;

            // Check if dash would hit something
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, dashDirection, moveAmount.magnitude, player.playerMovement.collisionMask);

            if (hit.collider != null)
            {
                // We hit something! Move only up to the point of contact
                targetPosition = hit.point;
                dashTimer = 0f; // End dash immediately because we hit a wall
            }

            player.transform.position = targetPosition;

            dashTimer -= Time.fixedDeltaTime;
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Player finished dashing");
    }

}
