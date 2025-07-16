using UnityEngine;

[CreateAssetMenu(fileName = "Skill data - ", menuName = "Game Setup/Skill Data")]
public class Skill_DataSO : ScriptableObject
{
      public int cost;

    [Header("Skill description")]
    public string displayName;
    [TextArea]
    public string description;
    public Sprite icon;

    // skill type that you should unlock
    
}
