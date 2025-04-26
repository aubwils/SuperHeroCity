using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator animator {get; private set;}
    public PlayerStateMachine stateMachine {get; private set;}
    public PlayerMovement playerMovement {get; private set;} 
    #endregion

    #region Player Stats
    [SerializeField] private bool isHero = false;
    #endregion


    #region States
    public PlayerIdleState idleState {get; private set;}
    public PlayerMoveState moveState {get; private set;}
    public PlayerDashState dashState {get; private set;}
    public PlayerTransformationState transformationState {get; private set;}


    #endregion

   private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();

        idleState = new PlayerIdleState(stateMachine, this, "IsIdle");
        moveState = new PlayerMoveState(stateMachine, this, "IsMoving");
        dashState = new PlayerDashState(stateMachine, this, "IsDashing");
        transformationState = new PlayerTransformationState(stateMachine, this, "IsTransforming");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        stateMachine.currentState.Update();

        // Temporary testing: Press L to transform
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartTransformation();
        }
    }

     private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public void OnTransformationAnimationComplete()
    {
        Debug.Log("OnTransformationAnimationComplete called");
        ToggleHeroIdentity();
        animator.SetBool("IsTransforming", false);
        stateMachine.ChangeState(idleState);
    }
    public void StartTransformation()
    {
        // Only allow if not already transforming
        if (stateMachine.currentState is PlayerTransformationState)
            return;

        stateMachine.ChangeState(transformationState); // Change to transformation state
    }

    private void ToggleHeroIdentity()
    {
       if (isHero)
        {
            isHero = false;
            animator.SetBool("IsHero", false);
        }
        else
        {
            isHero = true;
            animator.SetBool("IsHero", true);
        }
    }

     public bool GetPlayerIdentity()
    {
        return isHero; // Return the current identity of the player
    }

}
