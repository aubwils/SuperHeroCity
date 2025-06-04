using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrain : MonoBehaviour
{
    #region Components
    public CapsuleCollider2D myCollider {get; private set;}
    public Rigidbody2D rb { get; private set; }
    public Animator animator {get; protected set;}
    public CharacterFX characterFX {get; private set;}
    public CharacterStats characterStats {get; private set;}
    public StateMachine stateMachine {get; protected set;}
    public StateMachine StateMachine => stateMachine;

    #endregion

    [Header("Knockback Settings")]
    [SerializeField] protected float knockbackForce = 3f;
    [SerializeField] protected float knockbackDuration = 0.2f;
    public bool isKnockbacked { get; private set; }
  
    [Header("Combat Settings")]
    public float attackCheckRange = 1.0f;
    public float attackCheckOffset = .25f;
    public Transform meleeAttackCheck;

    public bool isBusy {get; private set;}



    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        myCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        characterFX = GetComponent<CharacterFX>();
        characterStats = GetComponent<CharacterStats>();
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

    public void TakeDamageEffect(Vector2 knockbackSource, float knockbackForce, float knockbackDuration)
    {
        characterFX.StartCoroutine("FlashFX");
        ApplyKnockback(knockbackSource, knockbackForce, knockbackDuration);
    }

    public void ApplyKnockback(Vector2 sourcePosition, float force, float duration)
    {
        if (isKnockbacked || isBusy) return;

        Vector2 direction = (transform.position - (Vector3)sourcePosition).normalized;
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    protected virtual IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration)
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
