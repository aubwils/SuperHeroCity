using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Player_Stats playerStats;
    public List<Inventory_EquipmentSlot> playerEquipmentList;

    protected override void Awake()
    {
        base.Awake();
        playerStats = GetComponent<Player_Stats>();
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

        EquipItem(inventoryItem, slotToReplace);
        UnEquipItem(itemToUnEquip);
    }

    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
    {
        slot.equipedItem = itemToEquip;
        slot.equipedItem.AddModifiers(playerStats);

        RemoveItemFromInventory(itemToEquip);
    }

    public void UnEquipItem(Inventory_Item itemToUnEquip)
    {
        if (CanAddItem() == false)
        {
            Debug.Log("No Space in Inventory!");
            return;
        }

        foreach (var slot in playerEquipmentList)
        {
            slot.equipedItem = null;
            break;
        }

        itemToUnEquip.RemoveModifiers(playerStats);
        AddItemToInventory(itemToUnEquip);
    }

}
