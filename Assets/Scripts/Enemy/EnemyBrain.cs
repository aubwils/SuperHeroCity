using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    #region Components
    public Animator animator {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public EnemyStateMachine stateMachine {get; private set;}
    public CharacterFX characterFX {get; private set;}
    public CharacterStats characterStats {get; private set;}

    
    #endregion

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

    public float attackCheckRange = .5f;
    public float attackCheckOffset = .25f;
    public Transform meleeAttackCheck;

    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Vector2 facingDirection = Vector2.down; // or from movement input

    public Transform PlayerTarget { get; private set; }
    #endregion

    [Header("Combat Settings")]
    [SerializeField] private float knockbackForce = 2f;
    [SerializeField] private float knockbackDuration = 0.2f;
    public bool isKnockbacked { get; private set; }

        

   public virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
        characterFX = GetComponent<CharacterFX>();
        characterStats = GetComponent<CharacterStats>();



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
        {
            facingDirection = dir.normalized;
            UpdateAttackCheckPosition();
        }
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
             Gizmos.DrawWireSphere(meleeAttackCheck.position, attackCheckRange);       
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

        public void UpdateAttackCheckPosition()
        {
            Vector2 offset = facingDirection.normalized * attackCheckOffset;
            meleeAttackCheck.localPosition = offset;
        }
    #endregion

    public void OnAnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void TakeDamage(Vector2 knockbackSource, float knockbackForce, float knockbackDuration)
    {
        characterFX.StartCoroutine("FlashFX");
        Debug.Log(gameObject.name + " took damage!");
        ApplyKnockback(knockbackSource, knockbackForce, knockbackDuration);
    }

    public void ApplyKnockback(Vector2 sourcePosition, float force, float duration)
    {
        if (isKnockbacked) return;

        Vector2 direction = (transform.position - (Vector3)sourcePosition).normalized;
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration)
    {
        isKnockbacked = true;

        rb.velocity = direction * force;

        yield return new WaitForSeconds(duration);

        rb.velocity = Vector2.zero;
        isKnockbacked = false;
    }

    public float GetKnockbackForce() => knockbackForce;
    public float GetKnockbackDuration() => knockbackDuration;


}
