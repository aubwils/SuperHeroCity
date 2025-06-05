using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; 
    [SerializeField] private bool canMove = true; // Whether the player can move

    [Header("Dash Settings")]
    public float dashDistance = 5f; // How far the player dashes
    public float dashSpeed = 20f;  // How fast the player dashes
    public float dashDuration = 0.2f; // How long the dash lasts
    public float dashCooldown = 1f;  // Cooldown time between dashes
    public float dashCooldownTimer = 0f; // Timer for the cooldown
    public bool canDash = true; // Whether the player can dash  
    [Header("Collision Settings")]
    public LayerMask collisionMask; // Layer mask for collision detection right now for dashing



    private PlayerInputActions playerInputActions;
    private Vector2 movementInput;
    private Vector2 lastMovementDirection; // Store the last non-zero movement direction
    private Rigidbody2D rb;
    private Player_Brain playerBrain;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerBrain = GetComponent<Player_Brain>();

        // Set the default starting direction to down
         lastMovementDirection = Vector2.down;
    }

    private void OnEnable()
    {
        playerInputActions.Movement.Enable();
        playerInputActions.Movement.Move.performed += OnMovePerformed;
        playerInputActions.Movement.Move.canceled += OnMoveCanceled;
        playerInputActions.Movement.Dash.performed += OnDashPerformed; 

    }

    private void OnDisable()
    {
        playerInputActions.Movement.Move.performed -= OnMovePerformed;
        playerInputActions.Movement.Move.canceled -= OnMoveCanceled;
        playerInputActions.Movement.Dash.performed -= OnDashPerformed; // Unsubscribe from Dash input
        playerInputActions.Movement.Disable();
    }
    private void Update()
    {
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime; // NOT fixedDeltaTime
            if (dashCooldownTimer <= 0f)
            {
                canDash = true;
                Debug.Log("Dash cooldown complete, can dash again!");
            }
        }
    }
    private void FixedUpdate()
    {        
        if (playerBrain.isKnockbacked || playerBrain.isBusy) return;

        if (canMove && !playerBrain.isBusy && playerBrain.StateMachine.currentState is Player_MoveState)
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

        rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
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
        if (!canDash) return; // Check if dashing is allowed
        if (!(playerBrain.StateMachine.currentState is Player_MoveState || playerBrain.StateMachine.currentState is Player_IdleState))
        return; // Only allow dash if in MoveState or IdleState

       
        playerBrain.StateMachine.ChangeState(playerBrain.dashState);
        canDash = false; // Disable dashing until cooldown is over
        dashCooldownTimer = dashCooldown; // Reset cooldown timer
        //Debug.Log("Player started dashing");
       
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
