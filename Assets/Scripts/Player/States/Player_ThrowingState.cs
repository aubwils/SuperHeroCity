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
        if (mainCamera != Camera.main)
            mainCamera = Camera.main;
    }

    public override void Update()
    {
        base.Update();

        Vector2 directionToMouse = DirectionToMouse();
        // Movement is currentl locked, need to add it so you can move while aiming and then will need to figur eout how to do animation for moving while aiming and then also should be able to change aim direction while movign based on where the mouse it.
        // turning movement... 
        // platofrmer tutorial does it like this:
        // playerbrain.HandleFlip(dirToMouse.x);
        playerBrain.HandleFacing(directionToMouse);




        if (playerBrain.playerInputActions.Player.Attack.WasPressedThisFrame())
        {
            playerBrain.animator.SetBool("ThrowPreformed", true);
            //skill manager create thrown item
        }

        if (playerBrain.playerInputActions.Player.RangeAttack.WasReleasedThisFrame() || animationTriggerCalled)
            stateMachine.ChangeState(playerBrain.idleState);


    }

    public override void Exit()
    {
        base.Exit();
        playerBrain.animator.SetBool("ThrowPreformed", false);

    }

    private Vector2 DirectionToMouse()
    {
        Vector2 playerPosition = playerBrain.transform.position;
        Vector2 worldMousePosition = mainCamera.ScreenToWorldPoint(playerBrain.mousePosition);

        Vector2 direction = worldMousePosition - playerPosition;

        return direction.normalized;
    }
}
