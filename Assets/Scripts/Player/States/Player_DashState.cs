using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : PlayerState
{
    private float dashTimer;
    private Vector2 dashDirection;
    private Vector2 dashTarget;

    public Player_DashState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName)
    : base(playerBrain, stateMachine, animBoolName)
    {
    }

   public override void Enter()
    {
        base.Enter();

        dashDirection = playerBrain.playerMovement.GetLastMovementDirection();
        if (dashDirection == Vector2.zero)
            dashDirection = Vector2.down;

        dashTimer = playerBrain.playerMovement.dashDuration;
        float dashDistance = playerBrain.playerMovement.dashSpeed * dashTimer;

        dashDirection.Normalize();

        // Use Collider2D.Cast to check for obstacles along the dash path
        RaycastHit2D[] hits = new RaycastHit2D[1]; // Only need the first hit
        int hitCount = playerBrain.myCollider.Cast(dashDirection, hits, dashDistance);

        if (hitCount > 0)
        {
            // Stop just before hitting
            float adjustedDistance = hits[0].distance - 0.05f; // buffer
            dashTarget = (Vector2)playerBrain.transform.position + dashDirection * adjustedDistance;

            dashTimer = adjustedDistance / playerBrain.playerMovement.dashSpeed;
        }
        else
        {
            // No hit, full dash
            dashTarget = (Vector2)playerBrain.transform.position + dashDirection * dashDistance;
        }

        Debug.DrawLine(playerBrain.transform.position, dashTarget, Color.red, 1f);
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer > 0f)
        {
            Vector2 newPos = Vector2.MoveTowards(playerBrain.transform.position, dashTarget, playerBrain.playerMovement.dashSpeed * Time.fixedDeltaTime);
            playerBrain.transform.position = newPos;

            dashTimer -= Time.fixedDeltaTime;
        }
        else
        {
            stateMachine.ChangeState(playerBrain.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Player finished dashing");
    }
}
