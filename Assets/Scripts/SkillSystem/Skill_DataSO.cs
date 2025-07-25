using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Skill data - ", menuName = "Game Setup/Skill Data")]
public class Skill_DataSO : ScriptableObject
{
    public int skillPointCost;
    public bool unlockedByDefault;
    public SkillType skillType;
    public UpgradeData upgradeData;

    [Header("Skill description")]
    public string skillName;
    [TextArea] public string description;
    public Sprite icon;

    //skill tye to unlock

}

[Serializable]
public class UpgradeData
{
    public SkillUpgradeType upgradeType;
    public float cooldown;
 }
