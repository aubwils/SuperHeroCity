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



    #region States
    public PlayerIdleState idleState {get; private set;}
    public PlayerMoveState moveState {get; private set;}
    public PlayerDashState dashState {get; private set;}


    #endregion

   private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();

        idleState = new PlayerIdleState(stateMachine, this, "IsIdle");
        moveState = new PlayerMoveState(stateMachine, this, "IsMoving");
        dashState = new PlayerDashState(stateMachine, this, "IsDashing");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        stateMachine.currentState.Update();
    }

     private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }
}
