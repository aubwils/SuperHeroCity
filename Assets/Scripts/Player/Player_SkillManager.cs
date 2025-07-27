using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public Skill_Dash dash { get; private set; }
    public Skill_Clone clone { get; private set; }
    public Skill_DeployableBomb deployableBomb { get; private set; } // tbd replacing with like a mine/bomb or robot/drone mine

    private void Awake()
    {
        dash = GetComponentInChildren<Skill_Dash>();
        clone = GetComponentInChildren<Skill_Clone>();
        deployableBomb = GetComponentInChildren<Skill_DeployableBomb>();
    }

    public Skill_Base GetSkillByType(SkillType type)
    {
        switch (type)
        {
            case SkillType.Dash: return dash;
            case SkillType.Clone: return clone;
            case SkillType.DeployableBomb: return deployableBomb; 
            default:
                Debug.Log($"Skill type {type} is not implemented yet.");
                return null;
        }
    }

}
