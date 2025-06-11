using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Entity_Health : MonoBehaviour, IDamageable
{
    private Slider healthBar;
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;
    private Entity_Brain entityBrain;

    [SerializeField] protected float maxHP = 100f;
    [SerializeField] public float currentHP;
    [SerializeField] protected bool isDead = false;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private Vector2 OnDamageKnockback = new Vector2(0.75f, 1.5f);
    [Space]
    [Range(0, 1)]
    [SerializeField] private float heavyDamageThreshold = .3f;
    [SerializeField] private float heavyKnockbackDuration = 0.3f;
    [SerializeField] private Vector2 onHeavyDamageKnockback = new Vector2(3f, 3f);


    public virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
        entityBrain = GetComponent<Entity_Brain>();
        healthBar = GetComponentInChildren<Slider>();
        currentHP = maxHP;
        UpdateHealthBar();
    }

    private void Start()
    {
    }

    public virtual bool TakeDamage(float damage, Transform damageSource)
    {
        if (isDead || AttackEvaded())
        {
            Debug.Log($"{gameObject.name} avoided damage (dead: {isDead}, evaded: {AttackEvaded()})");
            return false;
        }

        Debug.Log($"{gameObject.name} TOOK DAMAGE from {damageSource.name}");

        float duration = CalculateDuration(damage);
        Vector2 knockback = CalculateKnockback(damage, damageSource);

        entityVFX?.PlayOnDamageVFX();
        entityBrain?.ReciveKnockback(knockback, duration);
        ReduceHP(damage);
        DamageTextSpawnerManager.Instance.SpawnDamageText(Mathf.RoundToInt(damage), transform);

        return true;

    }

    protected void ReduceHP(float damage)
    {
        currentHP -= damage;
        UpdateHealthBar();
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

    private void UpdateHealthBar()
    {
        //to implement later when doing on screen UI. Player Will have a healthbar in the UI but not above their head. 
        //Enemies will NOT have health bar on their heads.
        //Boss enemies will have a health bar but possibly jsut at the top of the screen until defeated (similar to KH)
        //OR Could have a item player can wear to allow for hero ro see other enemies health bar on their heads? can turn it on/off in settings or 
        // unlock with a skill or item?
        if (healthBar == null)
            return;
        
        healthBar.value = currentHP / maxHP;
    }

    private bool AttackEvaded() => Random.Range(0, 100) < entityStats.GetEvasion();

    private Vector2 CalculateKnockback(float damage, Transform damageSource)
    {
              // Calculate direction from the damage source to this entity
        Vector2 direction = (transform.position - damageSource.position).normalized;
        // Apply knockback force using the serialized OnDamageKnockback magnitude
        Vector2 knockback = IsHeavyDamage(damage) ? direction * onHeavyDamageKnockback.magnitude : direction * OnDamageKnockback.magnitude;
        return knockback;
    }

    private float CalculateDuration(float damage)
    {
        return IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    }
    private bool IsHeavyDamage(float damage) => damage / maxHP > heavyDamageThreshold; 
}
