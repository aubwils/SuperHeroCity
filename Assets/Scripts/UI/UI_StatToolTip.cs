using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_StatToolTip : UI_ToolTip
{
    private Player_Stats playerStats;
    private TextMeshProUGUI statToolTipText;

    protected override void Awake()
    {
        base.Awake();
        playerStats = FindFirstObjectByType<Player_Stats>();
        statToolTipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowToolTip(bool show, RectTransform targetRect, StatType statType)
    {
        base.ShowToolTip(show, targetRect);
    }

    public string GetStatTooltip(StatType statType)
    {
        switch (statType)
        {
            //major stats
            case StatType.Strength:
                return "Increases Physical Damage by 1 per point." +
                "\n Increates critical power by 0.5% per point.";
            case StatType.Dexterity:
                return "Increases Critical Chance by 0.3% per point." +
                "\n Increates Evasion by 0.5% per point.";
            case StatType.Intelligence:
                return "Increases Elemental Resistance by 0.5% per point" +
                "\n Adds 1 elemental damage per point as a bonus." +
                "If all elements have 0 damage, the bonus will not be applied";
            case StatType.Constitution:
                return "Increases Maximum Health by 5 per point." +
                "\n Increases Armor by 1 per point.";

            case StatType.MaxHealth: return "Max Health Placeholder text";
            case StatType.ManaRegen: return "Max Regen Placeholder text";
            case StatType.MaxMana: return "Max Mana Placeholder text";
            case StatType.MaxStamina: return "Max Stamina Placeholder text";
            case StatType.Armor: return "Armor Placeholder text";
            case StatType.ArmorReduction: return "Armor Reduction  Placeholder text";
            case StatType.AttackSpeed: return "Attack Speed  Placeholder text";
            case StatType.CritChance: return "Crit Change  Placeholder text";
            case StatType.CritPower: return "Crit Power Placeholder text";
            case StatType.Damage: return "Damage Placeholder text";
            case StatType.DarkDamage: return "Dark Damage Placeholder text";
            case StatType.DarkResistance: return "Dark Resistance Placeholder text";
            case StatType.IceDamage: return "Ice Damage Placeholder text";
            case StatType.FireDamage: return "Fire Damage Placeholder text";
            case StatType.HolyDamage: return " Holy Damage Placeholder text";
            case StatType.PoisonDamage: return "Posion Damage Placeholder text";
            case StatType.LightningDamage: return "Lighting Damage Placeholder text";
            case StatType.Evasion: return "Evasion Placeholder text";
            case StatType.FireResistance: return "Fire Resistance Placeholder text";
            case StatType.HealthRegen: return "Health Regen Placeholder text";
            case StatType.HolyResistance: return "Holy Resistance Placeholder text";
            case StatType.IceResistance: return "Ice Resistance Placeholder text";
            case StatType.LightningResistance: return "Lighting Resistance Placeholder text";
            case StatType.PoisonResistance: return "Posion Resistance Placeholder text";
            case StatType.StaminaRegen: return "Stamina Regen Placeholder text";
            case StatType.SuspisionResistance: return "Suspision Resistance Placeholder text";
            case StatType.ElementalDamage: return "Elemental Damage Placeholder text";
//COME BACK TO Video 130 at 11:36

        }
    }
   
}
