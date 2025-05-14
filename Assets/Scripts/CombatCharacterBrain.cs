using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacterBrain : CharacterBrainBase
{

public float currentHealth = 10f;
    public float maxHealth = 10f;

    [Header("Combat")]
    public float attackDamage = 1f;
    public Transform attackCheck;
    public float attackCheckRadius = 0.5f;
    public float attackDistanceOffset = 0.25f;
    public CharacterFX characterFX { get; protected set; }

    [Header("Knockback")]
    [SerializeField] protected float knockbackForce = 5f;
    [SerializeField] protected float knockbackDuration = 0.5f;
    public bool isKnockbacked = false;
    public bool isInvincible = false;
    [SerializeField] protected float invincibilityDuration = 0f;

    public override void Awake()
    {
        base.Awake(); 
        characterFX = GetComponentInChildren<CharacterFX>();
    }

    public virtual void TakeDamage(float damage)
    {
         if (isInvincible || isKnockbacked) return;

        Debug.Log($"{gameObject.name} took {damage} damage.");
        if (characterFX != null)
            characterFX.StartCoroutine("FlashFX");
            
        StartCoroutine(ApplyInvincibility());
        StartCoroutine("Knockback", (Vector2)(transform.position - attackCheck.position));
    }
    protected virtual IEnumerator Knockback(Vector2 direction)
    {
        isKnockbacked = true;
        Vector2 knockbackDirection = direction.normalized * knockbackForce;
        rb.velocity = knockbackDirection; // Apply knockback in both x and y (or just x if preferred)
        
        yield return new WaitForSeconds(knockbackDuration);

        rb.velocity = Vector2.zero; // Reset velocity to stop sliding
        isKnockbacked = false;
    }
    protected virtual IEnumerator ApplyInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (attackCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
        }
    }

}
