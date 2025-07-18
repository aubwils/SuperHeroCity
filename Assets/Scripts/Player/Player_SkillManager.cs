using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public Skill_Dash dash { get; private set; }
    public Skill_Clone clone { get; private set; }

    private void Awake()
    {
        dash = GetComponentInChildren<Skill_Dash>();
        clone = GetComponentInChildren<Skill_Clone>();
    }
}
