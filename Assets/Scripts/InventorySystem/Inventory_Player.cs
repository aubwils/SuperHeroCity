using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Player_Brain playerBrain;
    public List<Inventory_EquipmentSlot> playerEquipmentList;

    protected override void Awake()
    {
        base.Awake();
        playerBrain = GetComponent<Player_Brain>();
    }

    public void TryEquipItem(Inventory_Item item)
    {   var eq = item.itemData.equipment;
        if (eq == null) return;

        var matchingSlots = playerEquipmentList.FindAll(s =>
            Array.Exists(eq.allowedSlots, allowed => allowed == s.slotType));

        foreach (var slot in matchingSlots)
        {
            if (!slot.HasItem())
            {
                EquipItem(item, slot);
                return;
            }
        }

        // Controller-friendly fallback: replace first matching
        var slotToReplace = matchingSlots[0];
        var itemToUnEquip = slotToReplace.equipedItem;

        UnEquipItem(itemToUnEquip, replacingItem: true);
        EquipItem(item, slotToReplace);
    }

    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
    {
        float savedHealthPercent = playerBrain.health.GetHealthPercent();
        slot.equipedItem = itemToEquip;
        slot.equipedItem.AddModifiers(playerBrain.entityStats);

        playerBrain.health.SetHealthToPercent(savedHealthPercent);
        RemoveItemFromInventory(itemToEquip);
        Debug.Log($"Max Health after equip: {playerBrain.entityStats.GetMaxHealth()}");
        Debug.Log($"Current Health after equip: {playerBrain.health.currentHealth}");

    }

    public void UnEquipItem(Inventory_Item itemToUnEquip, bool replacingItem = false)
    {
        if (CanAddItem() == false && replacingItem == false)
        {
            Debug.Log("No space to unequip!");
            return;
        }

        float savedHealthPercent = playerBrain.health.GetHealthPercent();
        // 1) Find the one slot that has this item
        var slotYoUnEquip = playerEquipmentList.Find(s => s.equipedItem == itemToUnEquip);

        if (slotYoUnEquip != null)
            slotYoUnEquip.equipedItem = null;

        // 2) Remove its stat modifiers
        itemToUnEquip.RemoveModifiers(playerBrain.entityStats);

        playerBrain.health.SetHealthToPercent(savedHealthPercent);
        // 4) Return the item to your bag
        AddItemToInventory(itemToUnEquip);
             Debug.Log($"Max Health after unequip: {playerBrain.entityStats.GetMaxHealth()}");
        Debug.Log($"Current Health after unequip: {playerBrain.health.currentHealth}");
    }

}
