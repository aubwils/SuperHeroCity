using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
     [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; 

    [Header("Dash Settings")]
    public float dashDistance = 5f; // How far the player dashes
    public float dashSpeed = 20f;  // How fast the player dashes
    public float dashDuration = 0.2f; // How long the dash lasts
    public float dashCooldown = 1f;  // Cooldown time between dashes
    public float dashCooldownTimer = 0f; // Timer for the cooldown
    public bool canDash = true; // Whether the player can dash  

    private PlayerInputActions playerInputActions;
    private Vector2 movementInput;
    private Vector2 lastMovementDirection; // Store the last non-zero movement direction
    private Rigidbody2D rb;
    private Animator animator;
    private Player player;

    // Hashed animator parameter names
    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();

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
        if (player.stateMachine.currentState is PlayerMoveState)
            {
                MovePlayer();
            }    
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
    }


    public void UpdateAnimatorMovementDirection()
    {
        Vector2 movementInput = GetMovementInput();
        Vector2 directionToAnimate = movementInput != Vector2.zero ? movementInput : lastMovementDirection;

        animator.SetFloat("MoveX", directionToAnimate.x);
        animator.SetFloat("MoveY", directionToAnimate.y);
    }

    private void OnDashPerformed(InputAction.CallbackContext ctx)
    { 
        if (!canDash) return; // Check if dashing is allowed
       
        player.stateMachine.ChangeState(player.dashState);
        canDash = false; // Disable dashing until cooldown is over
        dashCooldownTimer = dashCooldown; // Reset cooldown timer
        Debug.Log("Player started dashing");
       
    }

    public Vector2 GetLastMovementDirection()
    {
        return lastMovementDirection;
    }

}
