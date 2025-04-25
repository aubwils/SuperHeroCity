using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
     [Header("Movement Settings")]
    [SerializeField] private float speed = 5f; 

    private PlayerInputActions playerInputActions;
    private Vector2 movementInput;
    private Vector2 lastMovementDirection; // Store the last non-zero movement direction
    private Rigidbody2D rb;
    private Animator animator;

    // Hashed animator parameter names
    private static readonly int MoveXHash = Animator.StringToHash("MoveX");
    private static readonly int MoveYHash = Animator.StringToHash("MoveY");
    private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInputActions.Movement.Enable();
        playerInputActions.Movement.Move.performed += OnMovePerformed;
        playerInputActions.Movement.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        playerInputActions.Movement.Move.performed -= OnMovePerformed;
        playerInputActions.Movement.Move.canceled -= OnMoveCanceled;
        playerInputActions.Movement.Disable();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>().normalized; // Normalize input

        // Update the last movement direction if the input is not zero
        if (movementInput != Vector2.zero)
        {
            lastMovementDirection = movementInput;
        }

        UpdateAnimator(); // Update animation parameters
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        movementInput = Vector2.zero; // Reset input
        UpdateAnimator(); // Reset animation parameters
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
    }

    private void UpdateAnimator()
    {
        // Use the last movement direction when the player is not moving
        Vector2 directionToAnimate = movementInput != Vector2.zero ? movementInput : lastMovementDirection;

        animator.SetFloat(MoveXHash, directionToAnimate.x);
        animator.SetFloat(MoveYHash, directionToAnimate.y);
        animator.SetBool(IsMovingHash, movementInput != Vector2.zero);
    }
}
