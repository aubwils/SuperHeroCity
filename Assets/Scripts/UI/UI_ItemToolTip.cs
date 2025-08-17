using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemInfo;

    //overloading the show tool tip, with a base show tool tip function...
    //overload not override.
    public void ShowToolTip(bool show, RectTransform targetRect, Inventory_Item itemToShow)
    {
        base.ShowToolTip(show, targetRect);

        itemName.text = itemToShow.itemData.itemName;
        itemType.text = itemToShow.itemData.itemType.ToString();
        itemInfo.text = GetItemInfo(itemToShow);
    }

    public string GetItemInfo(Inventory_Item item)
    {
        if (item.itemData.itemType == ItemType.Material)
            return "Use for craafting.";

        if (item.itemData.itemType == ItemType.Consumable)
            return item.itemData.itemEffect.effectDescription;

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("");

        foreach (var modifier in item.modifiers)
        {
            string modifierType = GetStatNameByType(modifier.statType);
            string modifierValue = IsPercentageStat(modifier.statType) ? modifier.value.ToString() + "%" : modifier.value.ToString();
            stringBuilder.AppendLine("+ " + modifierValue + " " + modifierType);
        }

        return stringBuilder.ToString();
    }

    // THESE TWO FUNCTIONS ARE DUPLICATED IN UI_STAT SLOT... maybe make a class they can inherit from or pull from ??
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
