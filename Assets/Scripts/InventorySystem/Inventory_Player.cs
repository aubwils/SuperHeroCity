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
    {
        var inventoryItem = FindItemInInventory(item.itemData);
        var matchingSlots = playerEquipmentList.FindAll(slot => slot.slotType == item.itemData.itemType);

        //step 1 try to find empty slot and equip
        foreach (var slot in matchingSlots)
        {
            if (slot.HasItem() == false)
            {
                EquipItem(inventoryItem, slot);
                return;
            }
        }

        // step 2 no empty slots? replace first one 
        // I DO NOT WANT THIS... Want to have to do a drag and drop thing.
        //BUT may need to have it this way for console versions of game.. maybe I have this  Plus the drag and swap option? but if i have two ring slots and i want to change the 2nd one not the first one withthe new ring would this do that?
        //Is there a better way to solve this for console systems in the future?
        var slotToReplace = matchingSlots[0];
        var itemToUnEquip = slotToReplace.equipedItem;

        UnEquipItem(itemToUnEquip);
        EquipItem(inventoryItem, slotToReplace);
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

    public void UnEquipItem(Inventory_Item itemToUnEquip)
    {
        if (!CanAddItem())
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
