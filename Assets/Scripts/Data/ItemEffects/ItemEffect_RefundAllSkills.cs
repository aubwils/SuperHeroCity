using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Effect data - Refund All Skills ", menuName = "Game Setup/Item Data/Item Effect/Refund SKills Effect")]
public class ItemEffect_RefundAllSkills : ItemEffect_DataSO
{

    public override void ExecuteEffect()
    {
        UI ui = FindFirstObjectByType<UI>();
        ui.skillTreeUI.RefundAllSkills();
    }
    
}
