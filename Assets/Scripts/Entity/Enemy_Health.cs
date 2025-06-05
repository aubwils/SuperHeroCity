using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy_Brain enemyBrain => GetComponent<Enemy_Brain>();

    public override bool TakeDamage(float damage, Transform damageSource)
  {
    Debug.Log("Enemy took damage from: " + damageSource.name);

    if (damageSource.GetComponent<Player_Brain>() != null)
    {
        Debug.Log("Trying to chase after being hit!");
        enemyBrain.TryEnterChaseState(damageSource);
    }

    return base.TakeDamage(damage, damageSource);
}
}
