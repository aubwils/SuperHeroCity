using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple_Wanderer_Brain : Enemy_Brain
{
    #region States
    public Enemy_Simple_Wanderer_IdleState idleState { get; private set; }
    public Enemy_Simple_Wanderer_MoveState moveState { get; private set; }
    public Enemy_Simple_Wanderer_ChaseState chaseState { get; private set; }
    public Enemy_Simple_Wanderer_MeleeAttackState meleeAttackState { get; private set; }
    public Enemy_Simple_Wanderer_RecoveryState recoveryState { get; private set; }
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
        idleState = new Enemy_Simple_Wanderer_IdleState(this, stateMachine, "IsIdle", this);
        moveState = new Enemy_Simple_Wanderer_MoveState(this, stateMachine, "IsMoving", this);
        chaseState = new Enemy_Simple_Wanderer_ChaseState(this, stateMachine, "IsChasing", this);
        meleeAttackState = new Enemy_Simple_Wanderer_MeleeAttackState(this, stateMachine, "IsAttacking", this);
        recoveryState = new Enemy_Simple_Wanderer_RecoveryState(this, stateMachine, "IsIdle", this);
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

    public override void TryEnterChaseState(Transform playerTarget)
    {
        if (stateMachine.currentState == chaseState || stateMachine.currentState == meleeAttackState)
            return; // already chasing or attacking

       if (IsPlayerInSight())
        {
            SetPlayerTarget(playerTarget);
            stateMachine.ChangeState(chaseState);
        }
    }

}