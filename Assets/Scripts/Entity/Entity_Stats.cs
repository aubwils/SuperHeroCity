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

    public float GetPhisicalDamage(out bool isCrit) //Craeteinga variable only used in this method and out is saying you can get information OUT of this method when you call it.
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

        isCrit = Random.Range(0, 100) < totalCritChance; //bool variable is being decalred at the top.
        float finalDamage = isCrit ? totalBaseDamage * totalCritPower : totalBaseDamage;

        return finalDamage;
    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defenseStats.armor.GetValue();
        float bonusArmor = majorStats.constitution.GetValue(); // Bonus armor from constentoution: +1 per con
        float totalArmor = baseArmor + bonusArmor;

        //take armor reduction and calculate how much armor should be used
        float reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0,1); // if you pass 1-.4 = .6f .6 is the percentage of armor that will be used to defend on attack.
        // float reductionMultiplier = Mathf.Clamp01(1 - armorReduction); another way to say the above

        float effectiveArmor = totalArmor * reductionMultiplier; // Apply armor reduction to total armor

        float mitigation = effectiveArmor / (effectiveArmor + 100f); // Armor mitigation formula: Armor / (Armor + 100)
        float mitigationCap = 85f; // Max mitigation will be capped at 85%

        float finalMitigation = Mathf.Clamp(mitigation, 0f, mitigationCap); // Clamp to ensure it doesn't exceed the cap

        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        //Total armor reduction as multiplier (e.g 30/ 100 = .3f - multiplier)
        float finalReduction = offenseStats.armorReduction.GetValue() / 100f;

        return finalReduction;
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

    public float GetMaxHealth()
    {
        float baseMaxHP = maxHealth.GetValue();
        float bonusMaxHP = majorStats.constitution.GetValue() * 5; // Assuming 5 HP per point of constitution

        float finalMaxHP = baseMaxHP + bonusMaxHP;
        return finalMaxHP;
    }



}
