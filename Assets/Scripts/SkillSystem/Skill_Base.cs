using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Base : MonoBehaviour
{

    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType skillUpgradeType;
    [SerializeField] protected float cooldown;
    private float lastTimeUsed;
    protected Player_Brain playerBrain;


    protected virtual void Awake()
    {
        lastTimeUsed = lastTimeUsed - cooldown;
        playerBrain = GetComponent<Player_Brain>();
    }

    public virtual void TryUseSkill()
    {
        
    }

    public void SetSkillUpgrade(UpgradeData upgrade)
    {
        skillUpgradeType = upgrade.upgradeType;
        cooldown = upgrade.cooldown;
    }

    public bool CanUseSkill()
    {
        if(skillUpgradeType == SkillUpgradeType.None)
            return false;
            
        if (OnCooldown())
        {
            Debug.LogWarning("On Cooldown");
            return false;
        }

        return true;
    }

    protected bool Unlocked(SkillUpgradeType upgradeTocCheck) => skillUpgradeType == upgradeTocCheck;

    private bool OnCooldown() => Time.time < lastTimeUsed + cooldown;

    public void SetSkillOnCooldown() => lastTimeUsed = Time.time;

    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed = lastTimeUsed + cooldownReduction;

    public void ResetCooldown() => lastTimeUsed = Time.time;


}
