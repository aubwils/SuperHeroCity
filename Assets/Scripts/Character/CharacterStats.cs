using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
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


    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        if (AttackEvaded())
        {
            Debug.Log($" {gameObject.name} Attack Evaded!");
            return;
        }

        currentHealth -= offenseStats.damage.GetValue();
        Debug.Log("Took damage: " + offenseStats.damage.GetValue() + ", Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
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

        return baseEvasion + bonusEvasion;
    }

    private bool AttackEvaded() => Random.Range(0, 100) < GetEvasion();



}
