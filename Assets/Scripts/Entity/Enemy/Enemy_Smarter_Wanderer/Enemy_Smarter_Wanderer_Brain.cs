using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Smarter_Wanderer_Brain : Enemy_Brain
{
    [Header("Sight Settings")]
    [SerializeField] private float viewAngle = 90f; // degrees of cone (e.g. 90 for 45 left and right)
    [SerializeField] private float viewDistance = 3f; // how far enemy can see 

    #region States
    public Enemy_Smarter_Wanderer_IdleState idleState { get; private set; }
    public Enemy_Smarter_Wanderer_MoveState moveState { get; private set; }
    public Enemy_Smarter_Wanderer_ChaseState chaseState { get; private set; }
    public Enemy_Smarter_Wanderer_MeleeAttackState meleeAttackState { get; private set; }
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
        idleState = new Enemy_Smarter_Wanderer_IdleState(this, stateMachine, "IsIdle", this);
        moveState = new Enemy_Smarter_Wanderer_MoveState(this, stateMachine, "IsMoving", this);
        chaseState = new Enemy_Smarter_Wanderer_ChaseState(this, stateMachine, "IsChasing", this);
        meleeAttackState = new Enemy_Smarter_Wanderer_MeleeAttackState(this, stateMachine, "IsAttacking", this);
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
            return;

        SetPlayerTarget(playerTarget);

        Vector2 directionToPlayer = (playerTarget.position - transform.position).normalized;
        SetFacingDirection(directionToPlayer); // This updates facingDirection

        stateMachine.ChangeState(chaseState);
    }

    public override bool IsPlayerInSight()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, viewDistance, playerLayer);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Vector2 dirToPlayer = (hit.transform.position - transform.position).normalized;

                float angleToPlayer = Vector2.Angle(facingDirection, dirToPlayer);

                if (angleToPlayer < viewAngle / 2f)
                {
                    // Optional: Add line of sight check here if needed later
                    PlayerTarget = hit.transform;
                    return true;
                }
            }
        }

        PlayerTarget = null;
        return false;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.cyan;

        Vector3 leftBoundary = Quaternion.Euler(0, 0, -viewAngle / 2) * facingDirection;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, viewAngle / 2) * facingDirection;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

}
