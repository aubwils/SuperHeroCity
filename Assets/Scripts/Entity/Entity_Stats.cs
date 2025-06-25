using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public ResourceStats resourceStats;
    public MajorStats majorStats;
    public OffenseStats offenseStats;
    public DefenseStats defenseStats;

    public float GetElementalDamage(out ElementType elementType, float scaleFactor = 1f)
    {
        float fireDamage = offenseStats.fireDamage.GetValue();
        float iceDamage = offenseStats.iceDamage.GetValue();
        float lightningDamage = offenseStats.lightningDamage.GetValue();
        float poisonDamage = offenseStats.poisonDamage.GetValue();
        float holyDamage = offenseStats.holyDamage.GetValue();
        float darkDamage = offenseStats.darkDamage.GetValue();

        float bonusElementalDamage = majorStats.intelligence.GetValue(); // 1 point of intelligence gives 1 point of bonus elemental damage

        float highestDamage = fireDamage;
        elementType = ElementType.Fire;

        if (iceDamage > highestDamage)
        {
            highestDamage = iceDamage;
            elementType = ElementType.Ice;
        }
        if (lightningDamage > highestDamage)
        {
            highestDamage = lightningDamage;
            elementType = ElementType.Lightning;
        }
        if (poisonDamage > highestDamage)
        {
            highestDamage = poisonDamage;
            elementType = ElementType.Poison;
        }
        if (holyDamage > highestDamage)
        {
            highestDamage = holyDamage;
            elementType = ElementType.Holy;
        }
        if (darkDamage > highestDamage)
        {
            highestDamage = darkDamage;
            elementType = ElementType.Dark;
        }
        if (highestDamage <= 0)
        {
            elementType = ElementType.None; 
            return 0f;
        }

        float bonusFire = (fireDamage == highestDamage) ? 0 : fireDamage * .5f; // If fire damage is not the highest, give it a 50% bonus
        float bonusIce = (iceDamage == highestDamage) ? 0 : iceDamage * .5f; // If ice damage is not the highest, give it a 50% bonus
        float bonusLightning = (lightningDamage == highestDamage) ? 0 : lightningDamage * .5f; // If lightning damage is not the highest, give it a 50% bonus
        float bonusPoison = (poisonDamage == highestDamage) ? 0 : poisonDamage * .5f; // If poison damage is not the highest, give it a 50% bonus
        float bonusHoly = (holyDamage == highestDamage) ? 0 : holyDamage * .5f; // If holy damage is not the highest, give it a 50% bonus
        float bonusDark = (darkDamage == highestDamage) ? 0 : darkDamage * .5f; // If dark damage is not the highest, give it a 50% bonus

        float weakerElementsDamage = highestDamage + bonusFire + bonusIce + bonusLightning + bonusPoison + bonusHoly + bonusDark;
        float finalElementalDamage = highestDamage + weakerElementsDamage + bonusElementalDamage;

        return finalElementalDamage * scaleFactor; // Scale factor can be used to adjust the final damage output, e.g. for balancing purposes

    }

    public float GetElementalResistance(ElementType elementType)
    {
        float baseResistance = 0f;
        float bonusResistance = majorStats.intelligence.GetValue() * 0.5f; // 0.5% resistance per point of intelligence

        switch (elementType)
        {
            case ElementType.Fire:
                baseResistance = defenseStats.fireResistance.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defenseStats.iceResistance.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defenseStats.lightningResistance.GetValue();
                break;
            case ElementType.Poison:
                baseResistance = defenseStats.poisonResistance.GetValue();
                break;
            case ElementType.Holy:
                baseResistance = defenseStats.holyResistance.GetValue();
                break;
            case ElementType.Dark:
                baseResistance = defenseStats.darkResistance.GetValue();
                break;
            case ElementType.None:
                return 0f; // No resistance for None type
        }
        float totalResistance = baseResistance + bonusResistance;
        float resistanceCap = 75f; // Resistance cap at 75%
        float finalResistance = Mathf.Clamp(totalResistance, 0f, resistanceCap) / 100; // Clamp to ensure it doesn't exceed the cap
        return finalResistance;

    }

    [SerializeField] private float currentHealth;

    protected virtual void Start()
    {
        currentHealth = resourceStats.maxHealth.GetValue();

    }

    public virtual bool DoDamage(Entity_Stats targetStats)
    {
        // int totalDamage = offenseStats.damage.GetValue() + majorStats.strength.GetValue();
        // return targetStats.TakeDamage(totalDamage);
        return false;
    }

    public float GetPhisicalDamage(out bool isCrit, float scaleFactor = 1f) //Craeteinga variable only used in this method and out is saying you can get information OUT of this method when you call it.
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

        return finalDamage * scaleFactor; // Scale factor can be used to adjust the final damage output, e.g. for balancing purposes
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
        float baseMaxHP = resourceStats.maxHealth.GetValue();
        float bonusMaxHP = majorStats.constitution.GetValue() * 5; // Assuming 5 HP per point of constitution

        float finalMaxHP = baseMaxHP + bonusMaxHP;
        return finalMaxHP;
    }

    public Stat GetStatByType(StatType statType)
    {
        switch (statType)
        {
            // OffenseStats
            case StatType.AttackSpeed: return offenseStats.attackSpeed;
            case StatType.ArmorReduction: return offenseStats.armorReduction;
            case StatType.CritChance: return offenseStats.critChance;
            case StatType.CritPower: return offenseStats.critPower;
            case StatType.Damage: return offenseStats.damage;
            case StatType.DarkDamage: return offenseStats.darkDamage;
            case StatType.FireDamage: return offenseStats.fireDamage;
            case StatType.HolyDamage: return offenseStats.holyDamage;
            case StatType.IceDamage: return offenseStats.iceDamage;
            case StatType.LightningDamage: return offenseStats.lightningDamage;
            case StatType.PoisonDamage: return offenseStats.poisonDamage;

            // DefenseStats
            case StatType.Armor: return defenseStats.armor;
            case StatType.DarkResistance: return defenseStats.darkResistance;
            case StatType.Evasion: return defenseStats.evasion;
            case StatType.FireResistance: return defenseStats.fireResistance;
            case StatType.HolyResistance: return defenseStats.holyResistance;
            case StatType.IceResistance: return defenseStats.iceResistance;
            case StatType.LightningResistance: return defenseStats.lightningResistance;
            case StatType.PoisonResistance: return defenseStats.poisonResistance;
            case StatType.SuspisionResistance: return defenseStats.suspisionResistance;

            // MajorStats
            case StatType.Constitution: return majorStats.constitution;
            case StatType.Dexterity: return majorStats.dexterity;
            case StatType.Intelligence: return majorStats.intelligence;
            case StatType.Strength: return majorStats.strength;

            // ResourceStats
            case StatType.MaxHealth: return resourceStats.maxHealth;
            case StatType.HealthRegen: return resourceStats.healthRegen;
            case StatType.MaxMana: return resourceStats.maxMana;
            case StatType.ManaRegen: return resourceStats.manaRegen;
            case StatType.MaxStamina: return resourceStats.maxStamina;
            case StatType.StaminaRegen: return resourceStats.staminaRegen;

            default:
                Debug.LogWarning("Stat type not found: " + statType);
                return null;
        }
    }

}
