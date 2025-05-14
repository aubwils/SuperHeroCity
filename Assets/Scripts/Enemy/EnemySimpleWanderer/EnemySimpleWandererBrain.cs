using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererBrain : EnemyBrain
{
    #region States
    public EnemySimpleWandererIdleState idleState {get; private set;}   
    public EnemySimpleWandererMoveState moveState {get; private set;}   
    public EnemySimpleWandererChaseState chaseState {get; private set;}   
    public EnemySimpleWandererMeleeAttackState meleeAttackState {get; private set;}   
    public EnemySimpleWandererConfusedState confusedState {get; private set;}
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
        idleState = new EnemySimpleWandererIdleState( this, stateMachine, "IsIdle", this);
        moveState = new EnemySimpleWandererMoveState( this, stateMachine, "IsMoving", this);
        chaseState = new EnemySimpleWandererChaseState( this, stateMachine, "IsChasing", this);
        meleeAttackState = new EnemySimpleWandererMeleeAttackState( this, stateMachine, "IsAttacking", this);
        confusedState = new EnemySimpleWandererConfusedState( this, stateMachine, "IsIdle", this);
    }

    public override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    public override void Update()
    {
        base.Update();
    }

}