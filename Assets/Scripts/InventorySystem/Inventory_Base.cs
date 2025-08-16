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
    public void TryUseItem(Inventory_Item itemToUse)
    {
       // Only consumables are "used"
    if (itemToUse == null || !itemToUse.HasConsumableEffects())
        return;

    var stats = GetComponentInParent<Entity_Stats>();
    if (stats == null)
    {
        Debug.LogWarning("No Entity_Stats found for consumable use.");
        return;
    }

    // Apply all effects (run timed ones as coroutines)
    foreach (var effect in itemToUse.itemData.consumable.effects)
        StartCoroutine(ApplyConsumableEffect(stats, itemToUse, effect));

    // Decrement stack immediately
    if (itemToUse.stackSize > 1) itemToUse.RemoveStack();
    else RemoveItemFromInventory(itemToUse);

    OnInventoryChange?.Invoke();
    }

    private System.Collections.IEnumerator ApplyConsumableEffect(Entity_Stats stats, Inventory_Item item, StatEffect e)
    {
        var s = stats.GetStatByType(e.stat);

        if (e.durationSeconds <= 0f)
        {
            // Permanent: bump base value
            s.AddToBaseValue(e.value);
            yield break;
        }

        // Temporary: add/remove modifier by unique source
        string source = item.InstanceId + "_consumable_" + Guid.NewGuid().ToString("N");
        s.AddModifier(e.value, source);
        yield return new WaitForSeconds(e.durationSeconds);
        s.RemoveModifier(source);
    }

    public bool CanAddItem() => itemList.Count < maxInventorySize;

    // public bool CanAddToStack(Inventory_Item itemToAdd)
    // {
    //     List<Inventory_Item> stackableItems = itemList.FindAll(item => item.itemData == itemToAdd.itemData);
    //     foreach (var stack in stackableItems)
    //     {
    //         if (stack.CanAddStack())
    //             return true;
    //     }

    //     return false;
    // }

    // public Inventory_Item StackableItem(Inventory_Item itemToAdd)
    // {
    //     List<Inventory_Item> stackableItems = itemList.FindAll(item => item.itemData == itemToAdd.itemData);

    //     foreach (var stackableItem in stackableItems)
    //     {
    //         if (stackableItem.CanAddStack())
    //             return stackableItem;
    //     }
    //     return null;
    // }
    public bool CanAddToStack(Inventory_Item itemToAdd)
    {
        var data = itemToAdd.itemData;
        for (int i = 0; i < itemList.Count; i++)
        {
            var it = itemList[i];
            if (it.itemData == data && it.CanAddStack()) return true;
        }
        return false;
    }

    public Inventory_Item StackableItem(Inventory_Item itemToAdd)
    {
        var data = itemToAdd.itemData;
        for (int i = 0; i < itemList.Count; i++)
        {
            var it = itemList[i];
            if (it.itemData == data && it.CanAddStack()) return it;
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
        itemList.Remove(itemToRemove);
        OnInventoryChange?.Invoke();
    }

    public Inventory_Item FindItemInInventory(ItemDataSO itemData)
    {
        return itemList.Find(item => item.itemData == itemData);
    }

    public void TriggerUpdateUI() => OnInventoryChange?.Invoke();
}
