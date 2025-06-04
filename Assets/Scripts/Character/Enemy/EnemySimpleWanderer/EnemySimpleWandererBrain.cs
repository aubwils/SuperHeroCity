using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererBrain : EnemyBrain
{
    #region States
    public EnemySimpleWandererIdleState idleState { get; private set; }
    public EnemySimpleWandererMoveState moveState { get; private set; }
    public EnemySimpleWandererChaseState chaseState { get; private set; }
    public EnemySimpleWandererMeleeAttackState meleeAttackState { get; private set; }
    public EnemySimpleWandererRecoveryState recoveryState { get; private set; }
    #endregion

    #region Wanderer Settings
    [Header("Wanderer Settings")]
    [SerializeField] private float idleDuration = 2f;
    [SerializeField] private float minWanderTime = 1f;
    [SerializeField] private float maxWanderTime = 3f;

    public float IdleDuration => idleDuration;
    public float RandomWanderTime => Random.Range(minWanderTime, maxWanderTime);
    #endregion




    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemySimpleWandererIdleState(this, stateMachine, "IsIdle", this);
        moveState = new EnemySimpleWandererMoveState(this, stateMachine, "IsMoving", this);
        chaseState = new EnemySimpleWandererChaseState(this, stateMachine, "IsChasing", this);
        meleeAttackState = new EnemySimpleWandererMeleeAttackState(this, stateMachine, "IsAttacking", this);
        recoveryState = new EnemySimpleWandererRecoveryState(this, stateMachine, "IsIdle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
    }

    public override void TryEnterChaseState(Transform PlayerTarget)
    {
        if (stateMachine.currentState == chaseState || stateMachine.currentState == meleeAttackState)
            return; // already chasing or attacking

        this.PlayerTarget = PlayerTarget;
        stateMachine.ChangeState(chaseState);
    }

}