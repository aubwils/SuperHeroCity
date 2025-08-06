using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public ItemDataSO itemData;
    public int stackSize = 1; // default stack size when creating it is one by default

    public ItemModifier[] modifiers { get; private set; }

    //need constructor so can create item and add to inventory
    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;

        modifiers = EquipmentData()?.modifiers;
    }

    public void AddModifiers(Entity_Stats playerstats)
    {
        foreach (var mod in modifiers)
        {
            Stat statToModify = playerstats.GetStatByType(mod.statType);
            statToModify.AddModifier(mod.value, itemData.itemName); // will need to change this cant just use item name because dont want to be bale to use same item with the same name. for now ok
        }
    }

    public void RemoveModifiers(Entity_Stats playerstats)
    {
        foreach (var mod in modifiers)
        {
            Stat statToModify = playerstats.GetStatByType(mod.statType);
            statToModify.RemoveModifier(itemData.itemName); 

        }
    }

    private EquipmentDataSO EquipmentData()
    {
        if (itemData is EquipmentDataSO equipment)
            return equipment;

        return null;
    }

    public bool CanAddStack() => stackSize < itemData.maxStackSize;

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
