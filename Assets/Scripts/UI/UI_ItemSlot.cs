using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Inventory_Item itemInSlot { get; private set; }
    protected Inventory_Player playerInventory;
    protected UI ui;
    protected RectTransform uiTransform;


    [Header("UI Slot Setup")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Sprite defaultItemSlotImage;
    [SerializeField] private TextMeshProUGUI itemStackSize;

    protected void Awake()
    {
        ui = GetComponentInParent<UI>();
        uiTransform = GetComponent<RectTransform>();
        playerInventory = FindAnyObjectByType<Inventory_Player>();

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null || itemInSlot.itemData.itemType == ItemType.Material)
            return;
        if (itemInSlot.itemData.itemType == ItemType.Consumable)
        {
            if (itemInSlot.itemEffect.CanBeUsed() == false)
                return;
                
            playerInventory.TryUseItem(itemInSlot);
        }
        else
            playerInventory.TryEquipItem(itemInSlot);

        if (itemInSlot == null)
            ui.itemToolTip.ShowToolTip(false, null);
            //Still need to fix if you equip somehtign an dinventory changes and your mouse is on another item it still shows old tool tip.

    }

    public void UpdateSlot(Inventory_Item item)
    {
        itemInSlot = item;

        if (itemInSlot == null)
        {
            itemStackSize.text = "";
            itemIcon.sprite = defaultItemSlotImage;
            return;
        }

        Color color = Color.white; color.a = .9f;
        itemIcon.color = color;
        itemIcon.sprite = itemInSlot.itemData.itemIcon;
        itemStackSize.text = item.stackSize > 1 ? itemInSlot.stackSize.ToString() : "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInSlot == null) return;

        ui.itemToolTip.ShowToolTip(true, uiTransform, itemInSlot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.itemToolTip.ShowToolTip(false, null);
    }
}
