using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Skill_Dash : Skill_Base
{

    public void OnStartEffect()
    {
        if (Unlocked(SkillUpgradeType.Dash_CloneOnStart) || Unlocked(SkillUpgradeType.Dash_CloneOnStartAndEnd))
            CreateClone();

        if (Unlocked(SkillUpgradeType.Dash_SmashOnEnd) || Unlocked(SkillUpgradeType.Dash_SmashOnEndAndStart))
            CreateSmash();
    }

    public void OnEndEffect()
    {
        if (Unlocked(SkillUpgradeType.Dash_CloneOnStartAndEnd))
            CreateClone();

        if (Unlocked(SkillUpgradeType.Dash_SmashOnEndAndStart))
            CreateSmash();
    }

    private void CreateSmash()
    {
        Debug.Log("Smash Created");
        //skill manager smash create smash
    }

    private void CreateClone()
    {
        Debug.Log("Created a Clone");
        // skill manager clone create clone
    }
    
}
