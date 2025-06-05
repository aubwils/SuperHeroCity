using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamageable
{
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;
    private Entity_Brain entityBrain;

    [SerializeField] protected float maxHP = 100f;
    [SerializeField] public float currentHP;
    [SerializeField] protected bool isDead = false;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private Vector2 OnDamageKnockback = new Vector2(1.5f, 2.5f);

    public virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
        entityBrain = GetComponent<Entity_Brain>();
    }

    private void Start()
    {
        currentHP = maxHP;
    }

    public virtual bool TakeDamage(float damage, Transform damageSource)
    {
        if (isDead || AttackEvaded())
        { 
            Debug.Log($"{gameObject.name} avoided damage (dead: {isDead}, evaded: {AttackEvaded()})");
            return false;
        }

        Vector2 knockback = CalculateKnockback(damageSource);

Debug.Log($"{gameObject.name} taking {damage} damage from {damageSource.name}, knockback: {knockback}");
        entityVFX?.PlayOnDamageVFX();
        entityBrain?.ReciveKnockback(knockback, knockbackDuration);
        ReduceHP(damage);
        DamageTextSpawnerManager.Instance.SpawnDamageText(Mathf.RoundToInt(damage), transform);

        return true;

    }

    protected void ReduceHP(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log("Entity has died.");
    }

    private bool AttackEvaded() => Random.Range(0, 100) < entityStats.GetEvasion();

    private Vector2 CalculateKnockback(Transform damageSource)
    {
        // Calculate direction from the damage source to this entity
        Vector2 direction = (transform.position - damageSource.position).normalized;
        // Apply knockback force using the serialized OnDamageKnockback magnitude
        Vector2 knockback = direction * OnDamageKnockback.magnitude;
        return knockback;
    }
}
