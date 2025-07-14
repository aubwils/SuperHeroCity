using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirments;

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);

    }

    public void ShowToolTip(bool show, RectTransform targetRect, Skill_DataSO skillData)
    {
        base.ShowToolTip(show, targetRect);

        if (show == false)
            return;

        skillName.text = skillData.skillName;
        skillDescription.text = skillData.description;
        skillRequirments.text = $"Requirments: \n - {skillData.skillPointCost} Skill Points";

    }

}
