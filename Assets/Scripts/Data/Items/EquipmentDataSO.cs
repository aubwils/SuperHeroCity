using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Item data - ", menuName = "Game Setup/Equipment Item Data")]
public class EquipmentDataSO : ItemDataSO
{
    [Header("Item Modifiers")]
    public ItemModifier[] modifiers;

}

[Serializable]
public class ItemModifier
{
    public StatType statType;
    public float value;
}
