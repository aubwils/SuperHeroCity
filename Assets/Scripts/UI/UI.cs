using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillToolTip { get; private set; }
    public UI_ItemToolTip itemToolTip { get; private set; }
    public UI_StatToolTip statToolTip { get; private set; }

    public UI_SkillTree skillTreeUI { get; private set; }
    public UI_Inventory inventoryUI { get; private set; }


    private bool skillTreeUIEnabled;
    private bool inventoryUIEnabled;

    private void Awake()
    {
        itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
        statToolTip = GetComponentInChildren<UI_StatToolTip>();
        skillToolTip = GetComponentInChildren<UI_SkillToolTip>();

        skillTreeUI = GetComponentInChildren<UI_SkillTree>(true); //in case skill tree is disabled on start, if so need to add true to getcomponentinchildren otherwise it will not work.
        inventoryUI = GetComponentInChildren<UI_Inventory>(true);

        skillTreeUIEnabled = skillTreeUI.gameObject.activeSelf;
        inventoryUIEnabled = inventoryUI.gameObject.activeSelf;
    }

    public void ToggleInventoryUI()
    {
        inventoryUIEnabled = !inventoryUIEnabled;
        inventoryUI.gameObject.SetActive(inventoryUIEnabled);
        statToolTip.ShowToolTip(false, null);
        itemToolTip.ShowToolTip(false, null);
    }

    public void ToggleSkillTreeUI()
    {
        skillTreeUIEnabled = !skillTreeUIEnabled;
        skillTreeUI.gameObject.SetActive(skillTreeUIEnabled);
        skillToolTip.ShowToolTip(false, null); //hide tooltip when closing skill tree
    }
}
