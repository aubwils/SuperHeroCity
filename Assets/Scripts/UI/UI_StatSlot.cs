using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    private Player_Stats playerStats;
    private RectTransform myTransform;
    private UI ui;

    [SerializeField] private StatType statSlotType;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statValue;

    private void OnValidate()
    {
        gameObject.name = "UI_Stat - " + GetStatNameByType(statSlotType);
        statName.text = GetStatNameByType(statSlotType);
    }

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        myTransform = GetComponent<RectTransform>();
        playerStats = FindFirstObjectByType<Player_Stats>();
    }

    public void UpdateStatValue()
    {
        Stat statToUpdate = playerStats.GetStatByType(statSlotType);

        if (statToUpdate == null && statSlotType != StatType.ElementalDamage)
        {
            Debug.Log($"You do not have {statSlotType} implemented on the player!");
            return;
        }

        float value = 0;

        switch (statSlotType)
        {
            //Major Stats
            case StatType.Strength:
                value = playerStats.majorStats.strength.GetValue();
                break;
            case StatType.Dexterity:
                value = playerStats.majorStats.dexterity.GetValue();
                break;
            case StatType.Intelligence:
                value = playerStats.majorStats.intelligence.GetValue();
                break;
            case StatType.Constitution:
                value = playerStats.majorStats.constitution.GetValue();
                break;

            //Offensitve Stats
            case StatType.Damage:
                value = playerStats.GetBaseDamage();
                break;
            case StatType.CritChance:
                value = playerStats.GetCritChance();
                break;
            case StatType.CritPower:
                value = playerStats.GetCritPower();
                break;
            case StatType.ArmorReduction:
                value = playerStats.GetArmorReduction() * 100;
                break;
            case StatType.AttackSpeed:
                value = playerStats.offenseStats.attackSpeed.GetValue() * 100;
                break;

            //Defensive Stats
            case StatType.MaxHealth:
                value = playerStats.GetMaxHealth();
                break;
            case StatType.HealthRegen:
                value = playerStats.resourceStats.healthRegen.GetValue();
                break;
            case StatType.Evasion:
                value = playerStats.GetEvasion();
                break;
            case StatType.Armor:
                value = playerStats.GetBaseArmor();
                break;

            //Elemental Damage Stats
            case StatType.IceDamage:
                value = playerStats.offenseStats.iceDamage.GetValue(); // if do increased damage by intelligence/wisdom then eed to redo this like the get basedamagea and getbadearmor
                break;
            case StatType.FireDamage:
                value = playerStats.offenseStats.fireDamage.GetValue();
                break;
            case StatType.LightningDamage:
                value = playerStats.offenseStats.lightningDamage.GetValue();
                break;
            case StatType.PoisonDamage:
                value = playerStats.offenseStats.poisonDamage.GetValue();
                break;
            case StatType.HolyDamage:
                value = playerStats.offenseStats.holyDamage.GetValue();
                break;
            case StatType.DarkDamage:
                value = playerStats.offenseStats.darkDamage.GetValue();
                break;

            //Elemental Resistance Stats
            case StatType.IceResistance:
                value = playerStats.GetElementalResistance(ElementType.Ice) * 100; // if do increased resistance by intelligence/wisdom then eed to redo this like the get basedamagea and getbadearmor
                break; // or should damage be set up like getting element resistance and do get elemental damage?
            case StatType.FireResistance:
                value = playerStats.GetElementalResistance(ElementType.Fire) * 100;
                break;
            case StatType.LightningResistance:
                value = playerStats.GetElementalResistance(ElementType.Lightning) * 100;
                break;
            case StatType.PoisonResistance:
                value = playerStats.GetElementalResistance(ElementType.Poison) * 100;
                break;
            case StatType.HolyResistance:
                value = playerStats.GetElementalResistance(ElementType.Holy) * 100;
                break;
            case StatType.DarkResistance:
                value = playerStats.GetElementalResistance(ElementType.Dark) * 100;
                break;
        }
        statValue.text = IsPercentageStat(statSlotType) ? value + "%" : value.ToString();
    }




    // THESE TWO FUNCTIONS ARE DUPLICATED IN UI_ItemToolTip... maybe make a class they can inherit from or pull from ??
    private string GetStatNameByType(StatType statType)
    {
        switch (statType)
        {
            case StatType.MaxHealth: return "Max Health";
            case StatType.ManaRegen: return "Max Regen";
            case StatType.MaxMana: return "Max Mana";
            case StatType.MaxStamina: return "Max Stamina";
            case StatType.Armor: return "Armor";
            case StatType.ArmorReduction: return "Armor Reduction";
            case StatType.AttackSpeed: return "Attack Speed";
            case StatType.Constitution: return "Constitution";
            case StatType.CritChance: return "Crit Change";
            case StatType.CritPower: return "Crit Power";
            case StatType.Damage: return "Damage";
            case StatType.DarkDamage: return "Dark Damage";
            case StatType.DarkResistance: return "Dark Resistance";
            case StatType.Dexterity: return "Dexterity";
            case StatType.IceDamage: return "Ice Damage";
            case StatType.FireDamage: return "Fire Damage";
            case StatType.HolyDamage: return " Holy Damage";
            case StatType.PoisonDamage: return "Posion Damage";
            case StatType.LightningDamage: return "Lighting Damage";
            case StatType.ElementalDamage: return "Elemental Damage";
            case StatType.Evasion: return "Evasion";
            case StatType.FireResistance: return "Fire Resistance";
            case StatType.HealthRegen: return "Health Regen";
            case StatType.HolyResistance: return "Holy Resistance";
            case StatType.IceResistance: return "Ice Resistance";
            case StatType.Intelligence: return "Intelligence";
            case StatType.LightningResistance: return "Lighting Resistance";
            case StatType.PoisonResistance: return "Posion Resistance";
            case StatType.StaminaRegen: return "Stamina Regen";
            case StatType.SuspisionResistance: return "Suspision Resistance";
            case StatType.Strength: return "Strength";
            default: return "Unknown Stat";
        }
    }

     private bool IsPercentageStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.CritChance:
            case StatType.CritPower:
            case StatType.ArmorReduction:
            case StatType.IceResistance:
            case StatType.DarkResistance:
            case StatType.FireResistance:
            case StatType.HolyResistance:
            case StatType.PoisonResistance:
            case StatType.LightningResistance:
            case StatType.SuspisionResistance:
            case StatType.AttackSpeed:
            case StatType.Evasion:
                return true;
            default:
                return false;
            //any others give a % value?
        }
    }
    
}
