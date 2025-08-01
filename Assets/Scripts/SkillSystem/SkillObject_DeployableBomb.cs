using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_DeployableBomb : SkillObject_Base
{
    [SerializeField] private GameObject vfxPrefab;
    private Skill_DeployableBomb deployableBombManager;

    private Transform target;
    private float speed;

    private void Update()
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }

    public void MoveTowardsClosestTarget(float speed)
    {
        target = FindClosestTarget();
        this.speed = speed;
    }

    public void SetupDeployableBomb(Skill_DeployableBomb deployableBombManager)
    {
        this.deployableBombManager = deployableBombManager;

        playerStats = deployableBombManager.playerBrain.entityStats;
        damageScaleData = deployableBombManager.damageScaleData;

        float detonateTime = deployableBombManager.GetDetonateTime();
        Invoke(nameof(Explode), detonateTime);
    }

    public void Explode()
    {
        DamageEnemiesInRadius(transform, damageRadius);
        Instantiate(vfxPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Enemy_Brain>() == null)
            return;

        Explode();

    }
}
