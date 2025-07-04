using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Brain : MonoBehaviour
{
    #region Components
    public CapsuleCollider2D myCollider { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; protected set; }
    public Entity_VFX entityFX { get; private set; }
    public Entity_Stats entityStats { get; private set; }
    public StateMachine stateMachine { get; protected set; }
    public StateMachine StateMachine => stateMachine;

    #endregion

    private bool isKnockbacked = false;
    private Coroutine knockbackCoroutine;
    private Coroutine slowDownCoroutine;

    [Header("Combat Settings")]
    public float attackCheckRange = 1.0f;
    public float attackCheckOffset = .25f;
    public Transform meleeAttackCheck;

    public bool isBusy { get; private set; }



    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        myCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        entityFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
    }

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        stateMachine.currentState.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public virtual void CallAnimationFinishTrigger() => stateMachine.currentState.CallAnimationFinishTrigger();

    public virtual void SlowDownEntity(float duration, float slowMultiplier)
    {
        if (slowDownCoroutine != null)
            StopCoroutine(slowDownCoroutine);
            
        slowDownCoroutine = StartCoroutine(SlowDownEntityRoutine(duration, slowMultiplier));
    }

    protected virtual IEnumerator SlowDownEntityRoutine(float duration, float slowMultiplier)
    {
        yield return null;
    }

    public IEnumerator BusyFor(float duration)
    {
        isBusy = true;
        // Debug.Log("Player is busy for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);
        // Debug.Log("Player is no longer busy.");
        isBusy = false;
    }

    public void UpdateAttackCheckPosition(Vector2 direction)
    {
        Vector2 offset = direction.normalized * attackCheckOffset;
        meleeAttackCheck.localPosition = offset;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(meleeAttackCheck.position, attackCheckRange);       
    }


    public void ReciveKnockback(Vector2 knockback, float duration)
    {
        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        knockbackCoroutine = StartCoroutine(KnockbackRoutine(knockback, duration));
    }

    protected virtual IEnumerator KnockbackRoutine(Vector2 knockbackDirection, float duration)
    {
        isKnockbacked = true;

        rb.velocity = knockbackDirection;

        yield return new WaitForSeconds(duration);

        rb.velocity = Vector2.zero;
        isKnockbacked = false;
    }


    public bool GetKnockbackStatus() => isKnockbacked;
    // public float GetKnockbackDuration() => knockbackDuration;

    public virtual Vector2 GetFacingDirection()
    {
        return Vector2.down;
    }
}
