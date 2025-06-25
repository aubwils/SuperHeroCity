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

    [SerializeField] public float currentHealth;
    [SerializeField] protected bool isDead = false;
    [Header("Health Regen")] // Move health rate into stats??? anything else with health regen???
    [SerializeField] private float healthRegenRate = 1f; // how often you heal with health regen
    [SerializeField] private bool canRegenHealth = true;
    private Coroutine regenCoroutine;


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
        currentHealth = entityStats.GetMaxHealth(); ;
        UpdateHealthBar();
        

    }

    private void Start()
    {
    }

    public virtual bool TakeDamage(float damage, float elementalDamage, ElementType elementType, Transform damageSource)
    {
        if (isDead || AttackEvaded())
            return false;

        Entity_Stats attackerStats = damageSource.GetComponent<Entity_Stats>();
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0f;

        float mitigation = entityStats.GetArmorMitigation(armorReduction);
        float physicalDamageTaken = damage * (1 - mitigation); // e.g 150 damage, mitigation is .6 = 60%. you take 1 - .6 give you .40 so you take 60 damage. (150 x.40 = 60)

        float elementalResistance = entityStats.GetElementalResistance(elementType);
        float elementalDamageTaken = elementalDamage * (1 - elementalResistance);
        
        TakeKnockback(damageSource, physicalDamageTaken);
        ReduceHealth(physicalDamageTaken + elementalDamageTaken);
        DamageTextSpawnerManager.Instance.SpawnDamageText(Mathf.RoundToInt(physicalDamageTaken + elementalDamageTaken), transform);
        RegenerateHealth();
        return true;

    }

    private void RegenerateHealth()
    {
        if (!canRegenHealth || isDead)
            return;

        float regentAmount = entityStats.resourceStats.healthRegen.GetValue(); // amount to heal

        if (regenCoroutine != null)
            StopCoroutine(regenCoroutine);

        regenCoroutine = StartCoroutine(RegenerateHealthRoutine(healthRegenRate, regentAmount));
    }

    private IEnumerator RegenerateHealthRoutine(float healthRegenRate, float regentAmount)
    {
        //to add in a shold/can regenerate bool method, add conditionsl like not moving maybe?
        // or could have that for set locations that boost healing (like SDV spa) but leave a version that you cna move in for items?
        //have a skill set for passive regen and/or item and/or skill you hit to active? tbd
        while (canRegenHealth && !isDead && currentHealth < entityStats.GetMaxHealth())
        {
            IncreaseHealth(regentAmount);
            yield return new WaitForSeconds(healthRegenRate);
        }

        regenCoroutine = null;

    }
            

    public void IncreaseHealth(float healAmount)
    {
        if (isDead)
            return;

        float newHealth = currentHealth + healAmount;
        float maxHealth = entityStats.GetMaxHealth();

        // if (newHealth > maxHealth)
        //     currentHealth = maxHealth;
        // else
        //     currentHealth = newHealth;

        currentHealth = Mathf.Min(newHealth, maxHealth); // this will add the min of the two either newhealth or max health so if max health is the smaller number it will pick that and stay at the max. so similar to the if/else statment
        UpdateHealthBar();
    }

    public void ReduceHealth(float damage)
    {
        entityVFX?.PlayOnDamageVFX();
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        if (regenCoroutine != null)
            StopCoroutine(regenCoroutine);
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
        
        healthBar.value = currentHealth / entityStats.GetMaxHealth();
    }

    private bool AttackEvaded() => Random.Range(0, 100) < entityStats.GetEvasion();

    private void TakeKnockback(Transform damageSource, float finalDamage)
    {
        float duration = CalculateDuration(finalDamage);
        Vector2 knockback = CalculateKnockback(finalDamage, damageSource);

        entityBrain?.ReciveKnockback(knockback, duration);
    }

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

    private bool IsHeavyDamage(float damage) => damage / entityStats.GetMaxHealth() > heavyDamageThreshold; 
}
