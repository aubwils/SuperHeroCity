using System.Collections;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI ui;
    private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirments;

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private Color exampleColor;
    [SerializeField] private string lockedSkillText = "You've taken a different path - this skill is now locked.";

    private Coroutine textEffectCoroutine;

    protected override void Awake()
    {
        base.Awake();
        ui = GetComponentInParent<UI>();
        skillTree = ui.GetComponentInChildren<UI_SkillTree>();
    }

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);

    }

    public void ShowToolTip(bool show, RectTransform targetRect, UI_TreeNode treeNode)
    {
        base.ShowToolTip(show, targetRect);

        if (show == false)
            return;

        skillName.text = treeNode.skillData.skillName;
        skillDescription.text = treeNode.skillData.description;

        string skillLockedText = $"<color={importantInfoHex}>{lockedSkillText}</color>";
        string requirements = treeNode.isLocked ? skillLockedText : GetRequirements(treeNode.skillData.skillPointCost, treeNode.neededNodes, treeNode.conflictNodes);

        skillRequirments.text = requirements;
    }

    public void LockedSkillEffect()
    {
        if (textEffectCoroutine != null)
            StopCoroutine(textEffectCoroutine);
            
            textEffectCoroutine = StartCoroutine(TextBlinkEffectRoutine(skillRequirments, 0.15f, 3));
    }

    private IEnumerator TextBlinkEffectRoutine(TextMeshProUGUI text, float blinkInterval, int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            text.text = GetColoredText(notMetConditionHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);

            text.text = GetColoredText(importantInfoHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private string GetRequirements(int skillPointCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("Requirments:");

        string costColor = skillTree.EnoughSkillPoints(skillPointCost) ? metConditionHex : notMetConditionHex;
        stringBuilder.AppendLine($"<color={costColor}>- {skillPointCost} Skill Point(s)</color>");

        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            stringBuilder.AppendLine($"<color={nodeColor}>- {node.skillData.skillName}</color>");
        }

        if (conflictNodes.Length <= 0)
            return stringBuilder.ToString();

        stringBuilder.AppendLine(); // spapce between sections

        stringBuilder.AppendLine($"<color={importantInfoHex}>Locks out:</color>");

        foreach (var node in conflictNodes)
        {
            stringBuilder.AppendLine($"<color={importantInfoHex}>- {node.skillData.skillName}</color>");
        }
        return stringBuilder.ToString();


    }

    private string GetColoredText(string color, string text)
    {
        return $"<color={color}>{text}</color>";
    }
    


}
