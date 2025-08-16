using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    private readonly string itemId;
    public ItemDataSO itemData;
    public int stackSize = 1;

    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;
        itemId = itemData.itemName + " - " + Guid.NewGuid();
    }

    public void AddModifiers(Entity_Stats stats)
    {
        var eq = itemData.equipment;
        if (eq?.modifiers == null) return;

        foreach (var mod in eq.modifiers)
            stats.GetStatByType(mod.statType).AddModifier(mod.value, itemId);
    }

    public void RemoveModifiers(Entity_Stats stats)
    {
        var eq = itemData.equipment;
        if (eq?.modifiers == null) return;

        foreach (var mod in eq.modifiers)
            stats.GetStatByType(mod.statType).RemoveModifier(itemId);
    }

    public bool CanAddStack() => stackSize < itemData.maxStackSize;
    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;

    // Helper for consumables (used by Inventory_Base)
    public bool HasConsumableEffects() =>
        itemData != null &&
        itemData.itemCategory == ItemCategory.Consumable &&
        itemData.consumable != null &&
        itemData.consumable.effects != null &&
        itemData.consumable.effects.Count > 0;

    public string InstanceId => itemId; // expose for effect sources if you like
}