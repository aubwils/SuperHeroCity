using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;



    [SerializeField] private bool canMove = true; // Whether the player can move

    [Header("Dash Settings")]
    public float dashDistance = 5f; // How far the player dashes
    public float dashSpeed = 20f;  // How fast the player dashes
    public float dashDuration = 0.2f; // How long the dash lasts


    [Header("Collision Settings")]
    public LayerMask collisionMask; // Layer mask for collision detection right now for dashing



    private Vector2 movementInput;
    private Vector2 lastMovementDirection; // Store the last non-zero movement direction
    private Rigidbody2D rb;
    private Player_Brain playerBrain;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerBrain = GetComponent<Player_Brain>();

        // Set the default starting direction to down
        lastMovementDirection = Vector2.down;
    }

    private void Start()
    {
        playerBrain.playerInputActions.Player.Move.performed += OnMovePerformed;
        playerBrain.playerInputActions.Player.Move.canceled += OnMoveCanceled;
        playerBrain.playerInputActions.Player.Dash.performed += OnDashPerformed;
        playerBrain.playerInputActions.Player.Enable();

    }

    private void OnDisable()
    {
        playerBrain.playerInputActions.Player.Move.performed -= OnMovePerformed;
        playerBrain.playerInputActions.Player.Move.canceled -= OnMoveCanceled;
        playerBrain.playerInputActions.Player.Dash.performed -= OnDashPerformed; // Unsubscribe from Dash input
        playerBrain.playerInputActions.Player.Disable();
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (playerBrain.GetKnockbackStatus()) return;

        if (canMove && !playerBrain.isBusy && playerBrain.CanMoveInCurrentState())
        {
            MovePlayer();
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public Vector2 GetMovementInput()
    {
        return movementInput; // Ensure this is always up-to-date
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>().normalized; // Normalize input

        // Update the last movement direction if the input is not zero
        if (movementInput != Vector2.zero)
        {
            lastMovementDirection = movementInput;
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        movementInput = Vector2.zero; // Reset input
    }

    private void MovePlayer()
    {

        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
        playerBrain.UpdateAttackCheckPosition(lastMovementDirection);
    }


    public void UpdateAnimatorMovementDirection()
    {
        Vector2 movementInput = GetMovementInput();
        Vector2 directionToAnimate = movementInput != Vector2.zero ? movementInput : lastMovementDirection;

        playerBrain.animator.SetFloat("MoveX", directionToAnimate.x);
        playerBrain.animator.SetFloat("MoveY", directionToAnimate.y);
    }

    private void OnDashPerformed(InputAction.CallbackContext ctx)
    {
        if (!CanDash()) return;

        playerBrain.StateMachine.ChangeState(playerBrain.dashState);
        playerBrain.skillManager.dash.SetSkillOnCooldown(); // Set the dash skill on cooldown
    }

    private bool CanDash()
    {
        var dashSkill = playerBrain.skillManager.dash;
        if (!dashSkill.CanUseSkill()) return false; 

        var currentState = playerBrain.StateMachine.currentState;
        if (!(currentState is Player_MoveState || currentState is Player_IdleState)) return false; // Only allow dash if in MoveState or IdleState

        return true;
    }

    public void SetLastMovementDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            lastMovementDirection = direction;
        }
    }

    public Vector2 GetLastMovementDirection()
    {
        return lastMovementDirection;
    }

    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }
    
    
}
