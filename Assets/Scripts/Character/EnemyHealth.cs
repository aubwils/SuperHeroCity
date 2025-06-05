using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    private EnemyBrain enemyBrain => GetComponent<EnemyBrain>();

    public override bool TakeDamage(float damage, Transform damageSource)
  {
    Debug.Log("Enemy took damage from: " + damageSource.name);

    if (damageSource.GetComponent<Player>() != null)
    {
        Debug.Log("Trying to chase after being hit!");
        enemyBrain.TryEnterChaseState(damageSource);
    }

    return base.TakeDamage(damage, damageSource);
}
}
