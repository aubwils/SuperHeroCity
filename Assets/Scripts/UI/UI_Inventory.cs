using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    //this will be onthe parent object and collect all items in children.
    private Inventory_Player playerInventory;
    private UI_ItemSlot[] uiItemSlots;
    private UI_EquipmentSlot[] uiEquipmentSlots;

    [SerializeField] private Transform uiItemSlotParent;
    [SerializeField] private Transform uiEquipmentSlotParent;


    private void Awake()
    {
        uiItemSlots = uiItemSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        uiEquipmentSlots = uiEquipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

        playerInventory = FindFirstObjectByType<Inventory_Player>();
        playerInventory.OnInventoryChange += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateInventorySlots();
        UpdateEquipmentSlots();
    }


    private void UpdateEquipmentSlots()
    {
        List<Inventory_EquipmentSlot> playerEquipmentList = playerInventory.playerEquipmentList;

        for (int i = 0; i < uiEquipmentSlots.Length; i++)
        {
            var playerEquipmentSlot = playerEquipmentList[i];

            if (playerEquipmentSlot.HasItem() == false)
                uiEquipmentSlots[i].UpdateSlot(null);
            else
                uiEquipmentSlots[i].UpdateSlot(playerEquipmentSlot.equipedItem);
        }
    }

    private void UpdateInventorySlots()
    {
        List<Inventory_Item> itemList = playerInventory.itemList;

        for (int i = 0; i < uiItemSlots.Length; i++)
        {
            if (i < itemList.Count)
            {
                uiItemSlots[i].UpdateSlot(itemList[i]);
            }
            else
            {
                uiItemSlots[i].UpdateSlot(null);
            }
        }

    }

}
