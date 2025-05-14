using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : CombatCharacterBrain
{

    public EnemyStateMachine stateMachine {get; set;}
    [SerializeField] public EnemyEffectIcons effectIcons;    

    [Header("Enemy Movement")]
    [SerializeField] private float moveSpeed = 2.0f;
    public float MoveSpeed => moveSpeed;
    [SerializeField] private float chaseSpeed = 4.0f;
    public float ChaseSpeed => chaseSpeed;

    [Header("Enemy Attack Settings")]
    public float attackCooldown = 1.0f;
    [HideInInspector] public float lastAttackTime;

    #region Enemy Collision Checks
    [Header("Collision Checks Settings")]
    [SerializeField] private float obstacleCheckDistance = 0.5f;
    public float playerDetectRange = 3.0f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask playerLayer;
    public Transform PlayerTarget { get; private set; }

    [SerializeField] private Vector2 facingDirection = Vector2.down; // or from movement input
    #endregion
        

   public override void Awake()
    {
        base.Awake(); 
        stateMachine = new EnemyStateMachine();

    }

    public virtual void Start()
    {

    }
    public virtual void Update()
    {
        stateMachine.currentState.Update();
    }


    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    public void SetFacingDirection(Vector2 dir)
    {
        if (dir != Vector2.zero)
            facingDirection = dir.normalized;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    #region Collision Checks
        public bool IsObstacleAhead()
        {
            return Physics2D.Raycast(transform.position, facingDirection.normalized, obstacleCheckDistance, obstacleLayer);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            // Draw the obstacle check ray
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)facingDirection.normalized * obstacleCheckDistance);

            // Draw player detection range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, playerDetectRange);

        }


        public virtual bool IsPlayerInSight()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, playerDetectRange, playerLayer);
            if (hit != null && hit.CompareTag("Player"))
            {
                Vector2 directionToPlayer = (hit.transform.position - transform.position).normalized;
                float angle = Vector2.Angle(facingDirection, directionToPlayer);

                    RaycastHit2D ray = Physics2D.Raycast(transform.position, directionToPlayer, playerDetectRange, obstacleLayer);
                    if (ray.collider == null)
                    {
                        PlayerTarget = hit.transform;
                        return true;
                    }
            }

            PlayerTarget = null;
            return false;
        }

        public virtual bool IsPlayerInAttackRange()
        {
            Collider2D hit = Physics2D.OverlapCircle(attackCheck.position, attackCheckRadius, playerLayer);
            return hit != null;
        }
    #endregion
}
