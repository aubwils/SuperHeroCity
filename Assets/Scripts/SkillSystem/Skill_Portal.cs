using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Portal : Skill_Base
{
    private SkillObject_Portal currentPortal;

    [SerializeField] private GameObject gadgetPortalPrefab;
    [SerializeField] private GameObject mysticPortalPrefab;
    [SerializeField] private GameObject prefabToUse;

    [Header("Multi Bomb Upgrade")]
    [SerializeField] private int maxPortalAmount = 1;
    [SerializeField] private int currentPortalAmount;
    [SerializeField] private bool isBombReloading;
    //should only have 1 portal at a time, if calling portal again with one out it should teleport you back to it?
    // if portal out and leave area it shuld disappear. if so much time has passed and in the area it should disappear?
    [Header("Teleport Portal Upgrade")]
    [SerializeField] private float portalExistDuration = 10f;

    protected override void Awake()
    {
        base.Awake();
        currentPortalAmount = maxPortalAmount;
    }

    protected override void Start()
    {
    }

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
            return;

        if (Unlocked(SkillUpgradeType.Portal_Teleport))
            HandlePortalTeleport();

        // add a damage on teleport where player was
        //  and where they arrive
        // Portal stay open for longer
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

        currentPortal.transform.position = playerPosition;
        currentPortal.Disappear();

        playerBrain.TeleportPlayer(portalPosition);


    }

    //Portal should disappear if we go to a different scene too

    private void CreatePortal()
    {
        DeterminePrefabToUse();
        GameObject portal = Instantiate(prefabToUse, transform.position, Quaternion.identity);
        currentPortal = portal.GetComponent<SkillObject_Portal>();
        currentPortal.SetupPortal(portalExistDuration);
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
