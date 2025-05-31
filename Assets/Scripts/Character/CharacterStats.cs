using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable
{

    public Stat maxHealth;
    public bool isDead = false;


    public MajorStats majorStats;
    public OffenseStats offenseStats;
    public DefenseStats defenseStats;

    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();

    }

    public virtual void DoDamage(CharacterStats targetStats)
    {
        int totalDamage = offenseStats.damage.GetValue() + majorStats.strength.GetValue();
        targetStats.TakeDamage(totalDamage);
    }


    public virtual bool TakeDamage(int damage)
    {
        if (isDead) return false;

        if (AttackEvaded())
        {
            Debug.Log($" {gameObject.name} Attack Evaded!");
            return false;
        }

        currentHealth -= damage;
        DamageTextSpawnerManager.Instance.SpawnDamageText(damage, transform);
        Debug.Log("Took damage: " + damage + ", Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        return true;
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    public float GetMaxHealth()
    {
        float baseHP = maxHealth.GetValue();
        float bonusHP = majorStats.constitution.GetValue() * 5; // Assuming 5 HP per point of constitution

        return baseHP + bonusHP;
    }

    public float GetEvasion()
    {
        float baseEvasion = defenseStats.evasion.GetValue();
        float bonusEvasion = majorStats.dexterity.GetValue() * 0.5f; //  0.5% Evasion per point of dexterity

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 75f; // Evasion cap at 75%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0f, evasionCap); // Clamp to ensure it doesn't exceed the cap

        return finalEvasion;
    }

    private bool AttackEvaded() => Random.Range(0, 100) < GetEvasion();



}
