using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Base : MonoBehaviour
{
    public event Action OnInventoryChange;

    public int maxInventorySize = 10;
    public List<Inventory_Item> itemList = new List<Inventory_Item>();

    public bool CanAddItem() => itemList.Count < maxInventorySize;

    public void AddItem(Inventory_Item itemtoAdd)
    {

        Inventory_Item itemInInventory = FindItem(itemtoAdd.itemData);

        if (itemInInventory != null)
            itemInInventory.AddStack();
        else
            itemList.Add(itemtoAdd);

        OnInventoryChange?.Invoke();
    }

    public Inventory_Item FindItem(ItemDataSO itemData)
    {
        return itemList.Find(item => item.itemData == itemData && item.CanAddStack());
        // find all elements with the item dat we pass through and at the same time should be can add stack true
    }
}
