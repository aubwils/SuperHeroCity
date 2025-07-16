using UnityEngine;

[CreateAssetMenu(fileName = "Skill data - ", menuName = "Game Setup/Skill Data")]
public class Skill_DataSO : ScriptableObject
{
    public int skillPointCost = 1;

    [Header("Skill description")]
    public string skillName;
    [TextArea] public string description;
    public Sprite icon;

    //skill tye to unlock
    
}
