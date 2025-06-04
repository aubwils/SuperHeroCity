using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    private EnemyBrain enemyBrain => GetComponent<EnemyBrain>();

    public override bool TakeDamage(float damage, Transform damageSource)
    {
        if (damageSource.GetComponent<Player>() != null)
        {
            enemyBrain.TryEnterChaseState(damageSource);
        }
       return base.TakeDamage(damage, damageSource);
    }

}
