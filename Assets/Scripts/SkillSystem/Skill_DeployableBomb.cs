using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DeployableBomb : Skill_Base
{
    private SkillObject_DeployableBomb currentBomb;

    [SerializeField] private GameObject gadgetDeployableBombPrefab;
    [SerializeField] private GameObject mysticDeployableBombPrefab;
    [SerializeField] private GameObject prefabToUse;


    [SerializeField] private float detonateTime = 2f;

    [Header("Moving Bomb Upgrade")]
    [SerializeField] private float bombSpeed = 7f;

    [Header("Multi Bomb Upgrade")]
    [SerializeField] private int maxBombAmount = 3;
    [SerializeField] private int currentBombAmount;
    [SerializeField] private bool isBombReloading; // hmm for magic bombs could ahve them reload over time and for gadgets could do only reload once you build more.... maybe that should be more of a item... or we could say they are nano mini bots that come back to you after....
    // i feel like i want to go with the nano recreateion idea.. idk  if i like that they have to make the bots and ahve them.. although maybe I do tbd still I guess that would makeit more challenging for a gadget hero..

    protected override void Awake()
    {
        base.Awake();
        currentBombAmount = maxBombAmount;
    }

    protected override void Start()
    {
    }

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
            return;

        if (Unlocked(SkillUpgradeType.DeployableBomb))
            HandleDeployableBombRegular();

        if (Unlocked(SkillUpgradeType.DeployableBomb_MoveToEnemy))
            HandleDeployableBombMoving();

        if (Unlocked(SkillUpgradeType.DeployableBomb_MultiDeploy))
            HandleDeployableBombMultiDeploy();
    }

    private void HandleDeployableBombMultiDeploy()
    {
        if (currentBombAmount <= 0)
            return;

        CreateDeployableBomb();
        currentBomb.MoveTowardsClosestTarget(bombSpeed);
        currentBombAmount--;

        if (isBombReloading == false)
            StartCoroutine(BombReloadRoutine());
    }

    private IEnumerator BombReloadRoutine()
    {
        isBombReloading = true;

        while (currentBombAmount < maxBombAmount)
        {
            yield return new WaitForSeconds(cooldown);
            currentBombAmount++;
        }

        isBombReloading = false;

    }

    private void HandleDeployableBombMoving()
    {
        CreateDeployableBomb();
        currentBomb.MoveTowardsClosestTarget(bombSpeed);
        SetSkillOnCooldown();
    }

    public void HandleDeployableBombRegular()
    {
        CreateDeployableBomb();
        SetSkillOnCooldown();
    }

    public void CreateDeployableBomb()
    {
        DeterminePrefabToUse();
        //FUTURE - Moved to where we set orgin type on character creation and special/item/purchase/etc if let reset sometime in teh game. e.g. skillDeployableBomb.DeterminePrefabToUse();

        GameObject bomb = Instantiate(prefabToUse, transform.position, Quaternion.identity);
        currentBomb = bomb.GetComponent<SkillObject_DeployableBomb>();
        currentBomb.SetupDeployableBomb(detonateTime);
    }

    private void DeterminePrefabToUse()
    {
         switch (playerBrain.OriginType)
        {
            case OriginType.GadgetHero:
                prefabToUse = gadgetDeployableBombPrefab;
                break;

            case OriginType.MysticPowers:
                prefabToUse = mysticDeployableBombPrefab;
                break;

            default:
                prefabToUse = gadgetDeployableBombPrefab;
                break;
        }
    }
}
