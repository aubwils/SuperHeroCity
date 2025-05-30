using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats :  CharacterStats
{
    private EnemyBrain enemyBrain;
    [SerializeField] private GameObject enemyDeathEffectPrefab;

    protected override void Start()
    {
        base.Start();
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override bool TakeDamage(int damage)
    {
       return base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        Instantiate(enemyDeathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}