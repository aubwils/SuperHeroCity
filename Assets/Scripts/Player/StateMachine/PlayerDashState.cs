using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashTimer;
    private Vector2 dashDirection;
    private Vector2 dashTarget;

    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) 
        : base(stateMachine, player, animBoolName) { }

   public override void Enter()
    {
        base.Enter();

        dashDirection = player.playerMovement.GetLastMovementDirection();
        if (dashDirection == Vector2.zero)
            dashDirection = Vector2.down;

        dashTimer = player.playerMovement.dashDuration;
        float dashDistance = player.playerMovement.dashSpeed * dashTimer;

        dashDirection.Normalize();

        // Use Collider2D.Cast to check for obstacles along the dash path
        RaycastHit2D[] hits = new RaycastHit2D[1]; // Only need the first hit
        int hitCount = player.playerCollider.Cast(dashDirection, hits, dashDistance);

        if (hitCount > 0)
        {
            // Stop just before hitting
            float adjustedDistance = hits[0].distance - 0.05f; // buffer
            dashTarget = (Vector2)player.transform.position + dashDirection * adjustedDistance;

            dashTimer = adjustedDistance / player.playerMovement.dashSpeed;
        }
        else
        {
            // No hit, full dash
            dashTarget = (Vector2)player.transform.position + dashDirection * dashDistance;
        }

        Debug.DrawLine(player.transform.position, dashTarget, Color.red, 1f);
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer > 0f)
        {
            Vector2 newPos = Vector2.MoveTowards(player.transform.position, dashTarget, player.playerMovement.dashSpeed * Time.fixedDeltaTime);
            player.transform.position = newPos;

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
