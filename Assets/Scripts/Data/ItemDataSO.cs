using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item data - ", menuName = "Game Setup/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [Header("Core")]
    public string itemName;
    public Sprite itemIcon;
    public ItemCategory itemCategory;
    public int maxStackSize = 1;
    [TextArea] public string shortDescription;
    // Item Cost, Item Sell price, if sell able, 

    [Header("Traits")]
    public bool isGiftable = true; // gift is a trait, not a category
    // what would be other traits?

    [Header("Modules (optional)")]
    public EquipmentStats equipment;        // null if not equipment
    public ThrowableStats throwable;        // null if not throwable
    public ConsumableStats consumable;      // null if not consumable
                                            // furiture modules : placeable, rotateable, place on wall or floor or table, can have item placed on it, others?
                                            //Crafting modules? : Is craftable - items required to craft & # required. Is craftable material, can be used to craft other item? or is that limiting? unlockable craft?

    #if UNITY_EDITOR
    private void OnValidate()
    {
        // Simple guardrails; warnings only
        if (itemCategory == ItemCategory.Equipment && equipment == null)
            Debug.LogWarning($"{name}: category=Equipment but no Equipment module assigned.");
        if (itemCategory == ItemCategory.Consumable && (consumable == null || consumable.effects == null || consumable.effects.Count == 0))
            Debug.LogWarning($"{name}: category=Consumable but no consumable effects configured.");
    }
    #endif
}
    

[System.Serializable]
public class EquipmentStats
{
    [Tooltip("Which Equipment slots this item can be equipped to.")]
    public EquipSlot[] allowedSlots;
    public ItemModifier[] modifiers;              // design-time modifiers (StatType + value)
    public SkillUpgradeType[] unlockableSkills;   // optional skill unlocks granted by this item
}

[System.Serializable]
public class ThrowableStats
{
    public GameObject projectilePrefab;
    public float rotationOffset;  // e.g., -45 if your sprite is drawn tilted
    public bool canSpin;
    [Tooltip("Item must be picked up to use again or it will be lost")]
    public bool requiresPickup;
    public float throwSpeed = 10f;
    public float throwRange = 8f;
}

[System.Serializable]
public class ConsumableStats
{
    public List<StatEffect> effects;  // timed/permanent stat effects
}

[System.Serializable]
public class StatEffect
{
    public StatType stat;
    public float value;
    public float durationSeconds;     // <= 0 => permanent
}