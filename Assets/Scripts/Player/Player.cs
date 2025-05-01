using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    [SerializeField] private Animator heroAnimator;
    [SerializeField] private Animator seceretAnimator;
    public Animator animator {get; private set;}
    public PlayerStateMachine stateMachine {get; private set;}
    public PlayerMovement playerMovement {get; private set;} 

    [SerializeField] private GameObject seceretIdentityVisuals;
    [SerializeField] private GameObject heroIdentityVisuals;
    [HideInInspector] public CapsuleCollider2D playerCollider;

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
            playerCollider = GetComponent<CapsuleCollider2D>();

        stateMachine = new PlayerStateMachine();
        playerMovement = GetComponent<PlayerMovement>();

        idleState = new PlayerIdleState(stateMachine, this, "IsIdle");
        moveState = new PlayerMoveState(stateMachine, this, "IsMoving");
        dashState = new PlayerDashState(stateMachine, this, "IsDashing");
        transformationState = new PlayerTransformationState(stateMachine, this, "IsTransforming");

        animator = isHero ? heroAnimator : seceretAnimator; // Set the animator based on the player's identity
        heroIdentityVisuals.SetActive(isHero); // Show hero visuals if isHero is true
        seceretIdentityVisuals.SetActive(!isHero); // Show secret identity visuals if isHero is false
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
            seceretIdentityVisuals.SetActive(true);
            heroIdentityVisuals.SetActive(false);
            animator = seceretAnimator;
        }
        else
        {
            isHero = true;
            seceretIdentityVisuals.SetActive(false);
            heroIdentityVisuals.SetActive(true);
            animator = heroAnimator;
        }
    }


     public bool GetPlayerIdentity()
    {
        return isHero; // Return the current identity of the player
    }

}
