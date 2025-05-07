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

    #region Enemy Collision Checks
    [SerializeField] private float obstacleCheckDistance = 0.5f;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 facingDirection = Vector2.down; // or from movement input

    #endregion

    #region States



    #endregion

        

   private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {

    }
    private void Update()
    {

    }

     private void FixedUpdate()
    {
        
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public bool IsObstacleAhead()
    {
        return Physics2D.Raycast(transform.position, facingDirection.normalized, obstacleCheckDistance, obstacleLayer);
    }

    public bool IsPlayerInAttackRange()
    {
        return Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)facingDirection.normalized * obstacleCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void SetFacingDirection(Vector2 dir)
    {
        if (dir != Vector2.zero)
            facingDirection = dir.normalized;
    }

}
