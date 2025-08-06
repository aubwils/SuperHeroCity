using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public ItemDataSO itemData;
    public int stackSize = 1; // default stack size when creating it is one by default

    //need constructor so can create item and add to inventory
    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;
    }

    public bool CanAddStack() => stackSize < itemData.maxStackSize;

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
