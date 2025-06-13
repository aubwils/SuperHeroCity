using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
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

    public virtual bool DoDamage(Entity_Stats targetStats)
    {
        // int totalDamage = offenseStats.damage.GetValue() + majorStats.strength.GetValue();
        // return targetStats.TakeDamage(totalDamage);
        return false;
    }

    public float GetPhisicalDamage()
    {
        float baseDamage = offenseStats.damage.GetValue();
        float bonusDamage = majorStats.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offenseStats.critChance.GetValue();
        float bonusCritChance = majorStats.dexterity.GetValue() * 0.3f; // +0.3% Crit Chance per point of dexterity 
        float totalCritChance = baseCritChance + bonusCritChance;

        float baseCritPower = offenseStats.critPower.GetValue();
        float bonusCritPower = majorStats.strength.GetValue() * 0.5f; // +0.5% Crit Damage per point of strength
        float totalCritPower = (baseCritPower + bonusCritPower) / 100f; // Convert to a multiplier

        bool isCrit = Random.Range(0, 100) < totalCritChance;
        float finalDamage = isCrit ? totalBaseDamage * totalCritPower : totalBaseDamage;

        return finalDamage;
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




}
