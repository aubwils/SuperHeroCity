using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Portal : Skill_Base
{
    private SkillObject_Portal currentPortal;

    [Header("Teleport Portal Settings")]
    [SerializeField] private GameObject gadgetPortalPrefab;
    [SerializeField] private GameObject mysticPortalPrefab;
    private GameObject prefabToUse;
    [SerializeField] private float portalExistDuration = 10f;

    [Header("Heal Upgrade Settings")]
    public float savedHealthAtPortalPlacement;
    [Range (0, 1)]
    [SerializeField] private float healPercentage = 0.3f; // 30% heal

    
    [Header("AOE Upgrade Settings")]
    [SerializeField] private float aoeRadius = 3f;
    [SerializeField] private float aoeDamage = 50f; //not used need to look at, if aoe multiplpe if damage is based on player level and damge then maybe there is a multiplier or des skill scale damage cover this?
    [SerializeField] private GameObject aoeVFXPrefab;

    Player_Health playerHealth;



    protected override void Awake()
    {
        base.Awake();
        playerHealth = GetComponentInParent<Player_Health>();
    }

    protected override void Start()
    {
    }

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
            return;

        if (Unlocked(SkillUpgradeType.Portal_Teleport) || Unlocked(SkillUpgradeType.Portal_TeleportAndAOEArrival) ||
        Unlocked(SkillUpgradeType.Portal_TeleportAndAOEBehind) || Unlocked(SkillUpgradeType.Portal_TeleportAndHeal) || Unlocked(SkillUpgradeType.Portal_TeleportTimeRewindHeal))
            HandlePortalTeleport();

        // Add increase Portal exists durration
        //Origin Type should set portalExistDuration too.
    }

    private void HandlePortalTeleport()
    {
        if (currentPortal == null)
        {
            CreatePortal();
        }
        else
        {
            SwapPlayerandPortal();
            SetSkillOnCooldown();
        }
    }

    private void SwapPlayerandPortal()
    {
        //swapped theplayer and portal object
        // Vector3 portalPosition = currentPortal.transform.position;
        // Vector3 playerPosition = playerBrain.transform.position;

        // currentPortal.transform.position = playerPosition;
        // playerBrain.TeleportPlayer(portalPosition);


        Vector3 portalPosition = currentPortal.transform.position;
        Vector3 playerPosition = playerBrain.transform.position;

        if (Unlocked(SkillUpgradeType.Portal_TeleportAndAOEBehind) || Unlocked(SkillUpgradeType.Portal_TeleportAndAOEArrival))
            CreateAOEEffect(playerPosition);

        currentPortal.transform.position = playerPosition;
        currentPortal.Disappear();

        playerBrain.TeleportPlayer(portalPosition);

        if (Unlocked(SkillUpgradeType.Portal_TeleportAndAOEArrival))
            CreateAOEEffect(portalPosition);

        if (Unlocked(SkillUpgradeType.Portal_TeleportAndHeal)) 
        {
            float healAmount = playerHealth.currentHealth * healPercentage;
            playerHealth.IncreaseHealth(healAmount);
        }

        if (Unlocked(SkillUpgradeType.Portal_TeleportTimeRewindHeal))
        {
            playerHealth.ForceHealthOverride(savedHealthAtPortalPlacement);
        }
            
    }


    private void CreateAOEEffect(Vector3 position)
    {
        GameObject aoe = Instantiate(aoeVFXPrefab, position, Quaternion.identity);
        SkillObject_DeployableBomb currentAOE = aoe.GetComponent<SkillObject_DeployableBomb>();
        
        // Immediately explode (no timer, no movement)
        currentAOE.Explode();
    }

    //Portal should disappear if we go to a different scene too
    private void CreatePortal()
    {
        DeterminePrefabToUse();
        GameObject portal = Instantiate(prefabToUse, transform.position, Quaternion.identity);
        currentPortal = portal.GetComponent<SkillObject_Portal>();
        currentPortal.SetupPortal(portalExistDuration);

        if (Unlocked(SkillUpgradeType.Portal_TeleportTimeRewindHeal))
        {
            savedHealthAtPortalPlacement = playerBrain.GetComponent<Player_Health>().currentHealth;
        }

    }

    private void DeterminePrefabToUse()
    {
        switch (playerBrain.OriginType)
        {
            case OriginType.GadgetHero:
                prefabToUse = gadgetPortalPrefab;
                break;

            case OriginType.MysticPowers:
                prefabToUse = mysticPortalPrefab;
                break;

            default:
                prefabToUse = gadgetPortalPrefab;
                break;
        }
    }

}
