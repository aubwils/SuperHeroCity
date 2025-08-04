using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player_ThrowingState : PlayerState
{
    private Camera mainCamera;

    public Player_ThrowingState(Player_Brain playerBrain, StateMachine stateMachine, string animBoolName) : base(playerBrain, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        skillManager.throwableObject.EnableDots(true);

        if (mainCamera != Camera.main)
            mainCamera = Camera.main;
    }

    public override void Update()
    {
        base.Update();

        Vector2 directionToMouse = DirectionToMouse();
        playerBrain.HandleFacing(directionToMouse);

        skillManager.throwableObject.PredictTrajectory(directionToMouse);

        if (playerBrain.playerInputActions.Player.Attack.WasPressedThisFrame())
        {
            playerBrain.animator.SetBool("ThrowPreformed", true);
            skillManager.throwableObject.EnableDots(false);
            skillManager.throwableObject.ConfirmTrajectory(directionToMouse);
            //skill manager create thrown item
        }

        if (playerBrain.playerInputActions.Player.RangeAttack.WasReleasedThisFrame() || animationTriggerCalled)
            stateMachine.ChangeState(playerBrain.idleState);


    }

    public override void Exit()
    {
        base.Exit();
        playerBrain.animator.SetBool("ThrowPreformed", false);
        skillManager.throwableObject.EnableDots(false);


    }

    private Vector2 DirectionToMouse()
    {
        Vector2 playerPosition = playerBrain.transform.position;
        Vector2 worldMousePosition = playerBrain.GetWorldMousePosition();

        Vector2 direction = worldMousePosition - playerPosition;

        return direction.normalized;
    }
}
