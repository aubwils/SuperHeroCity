using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shard : Skill_Base
{
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonateTime = 2f;

    public void CreateShard()
    {
        if (skillUpgradeType == SkillUpgradeType.none)
            return;

        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        shard.GetComponent<SkillObject_Shard>().SetupShard(detonateTime);
        //Think will replace this with a mine skill or robot drone skill or maybe a elementeal summon... leaning towards drone/mine
    }
}
