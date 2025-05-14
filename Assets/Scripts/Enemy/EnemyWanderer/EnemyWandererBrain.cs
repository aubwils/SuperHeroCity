using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandererBrain : EnemyBrain 
{
    #region States
    public EnemyWandererIdleState idleState { get; private set; }
    public EnemyWandererWanderState wanderState { get; private set; }
    public EnemyWandererChaseState chaseState { get; private set; }
    public EnemyWandererAttackState attackState { get; private set; }
    #endregion

    #region Wanderer Settings
    [Header("Wanderer Settings")]
    [SerializeField] private float idleDuration = 2f;
    [SerializeField] private float minWanderTime = 1f;
    [SerializeField] private float maxWanderTime = 3f;

    public float IdleDuration => idleDuration;
    public float RandomWanderTime => Random.Range(minWanderTime, maxWanderTime);
    #endregion

    public override void Awake()
    {
        base.Awake();

        idleState = new EnemyWandererIdleState(this, stateMachine, "IsIdle", this);
        wanderState = new EnemyWandererWanderState(this, stateMachine, "IsMoving", this);
        chaseState = new EnemyWandererChaseState(this, stateMachine, "IsChasing", this);
        attackState = new EnemyWandererAttackState(this, stateMachine, "IsAttacking", this);
        
    }

   public override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState); 
    }

   public override void Update()
    {
        float distanceToPlayer = PlayerTarget != null ? Vector2.Distance(transform.position, PlayerTarget.position) : float.MaxValue; 

        if (IsPlayerInAttackRange() && stateMachine.currentState != attackState)
        {
            stateMachine.ChangeState(attackState);
        }
        else if (IsPlayerInSight() && stateMachine.currentState != chaseState)
        {
            stateMachine.ChangeState(chaseState);
        }

        stateMachine.currentState.Update();
    }


    public void Move(Vector2 direction)
    {
        rb.velocity = direction;
    }


}
