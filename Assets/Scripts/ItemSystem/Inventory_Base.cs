using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory_Base : MonoBehaviour
{
    public event Action OnInventoryChange;

    public int maxInventorySize = 10;
    public List<Inventory_Item> itemList = new List<Inventory_Item>();

    protected virtual void Awake()
    {
        
    }

    public bool CanAddItem() => itemList.Count < maxInventorySize;
    public bool CanAddToStack(Inventory_Item itemToAdd)
    {
        List<Inventory_Item> stackableItems = itemList.FindAll(item => item.itemData == itemToAdd.itemData);
        foreach (var stack in stackableItems)
        {
            if (stack.CanAddStack())
                return true;
        }

        return false;
    }

    public Inventory_Item StackableItem(Inventory_Item itemToAdd)
    {
        List<Inventory_Item> stackableItems = itemList.FindAll(item => item.itemData == itemToAdd.itemData);

        foreach (var stackableItem in stackableItems)
        {
            if (stackableItem.CanAddStack())
                return stackableItem;
        }
        return null;
    }

    public void AddItemToInventory(Inventory_Item itemtoAdd)
    {

        Inventory_Item itemInInventory = FindItemInInventory(itemtoAdd.itemData);
        // look for an existing stack of *this* item
        var existingStackable = StackableItem(itemtoAdd);

        if (existingStackable != null)
            existingStackable.AddStack();
        else
            itemList.Add(itemtoAdd);

        OnInventoryChange?.Invoke();
    }

    public void RemoveItemFromInventory(Inventory_Item itemToRemove)
    {
        itemList.Remove(FindItemInInventory(itemToRemove.itemData));
        OnInventoryChange?.Invoke();
    }

    public Inventory_Item FindItemInInventory(ItemDataSO itemData)
    {
        return itemList.Find(item => item.itemData == itemData);
    }
}
