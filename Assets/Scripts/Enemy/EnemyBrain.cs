using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    #region Components
    public Animator animator {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public EnemyStateMachine stateMachine {get; private set;}
    
    #endregion

    #region Enemy Stats
    [SerializeField] private float moveSpeed = 2.0f;
    public float MoveSpeed => moveSpeed;
      [SerializeField] private float chaseSpeed = 4.0f;
    public float ChaseSpeed => chaseSpeed;
    public float currentMoveSpeed;
    #endregion

    #region Enemy Collision Checks
    [SerializeField] private float obstacleCheckDistance = 0.5f;
    [SerializeField] private float playerDetectRange = 3.0f;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 facingDirection = Vector2.down; // or from movement input
    public Transform PlayerTarget { get; private set; }

    #endregion
        

   public virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;

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

    #region Collision Checks
        public bool IsObstacleAhead()
        {
            return Physics2D.Raycast(transform.position, facingDirection.normalized, obstacleCheckDistance, obstacleLayer);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)facingDirection.normalized * obstacleCheckDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, playerDetectRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }


        public virtual bool IsPlayerInSight()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, playerDetectRange, playerLayer);
            if (hit != null && hit.CompareTag("Player"))
            {
                PlayerTarget = hit.transform;
                return true;
            }

            PlayerTarget = null;
            return false;
        }

        public virtual bool IsPlayerInAttackRange()
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
            return hit != null;
        }
    #endregion
}
