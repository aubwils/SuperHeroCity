using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : CharacterBrain
{

    #region Enemy Stats
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2.0f;
    public float MoveSpeed => moveSpeed;
      [SerializeField] private float chaseSpeed = 4.0f;
    public float ChaseSpeed => chaseSpeed;
    public float currentMoveSpeed;
    #endregion

    #region Enemy Collision Checks
    [Header("Collision Checks")]
    [SerializeField] private float obstacleCheckDistance = 0.5f;
    [SerializeField] private float playerDetectRange = 3.0f;

    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask playerLayer;
    public Transform PlayerTarget { get; private set; }
   
    [SerializeField] private Vector2 facingDirection = Vector2.down; // or from movement input

    #endregion

    [Header("Combat Settings")]
    public float timeBetweenAttacks = 1f;

        

   protected override void Awake()
    {
        base.Awake();
        currentMoveSpeed = moveSpeed;
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    public void SetFacingDirection(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
            facingDirection = dir.normalized;
            UpdateAttackCheckPosition(facingDirection);
        }
    }

    #region Collision Checks
    public bool IsObstacleAhead()
    {
        return Physics2D.Raycast(transform.position, facingDirection.normalized, obstacleCheckDistance, obstacleLayer);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)facingDirection.normalized * obstacleCheckDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
 
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
        Collider2D hit = Physics2D.OverlapCircle(meleeAttackCheck.position, attackCheckRange, playerLayer);
        return hit != null;
    }

    #endregion


}
