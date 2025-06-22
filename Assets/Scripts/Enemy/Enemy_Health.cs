using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : Entity_Health
{
  private Enemy_Brain enemyBrain => GetComponent<Enemy_Brain>();
  [SerializeField] private GameObject enemyDeathEffectPrefab;

  public override bool TakeDamage(float damage, float elementalDamage, ElementType elementType, Transform damageSource)
  {

    if (damageSource.GetComponent<Player_Brain>() != null)
        enemyBrain.TryEnterChaseState(damageSource);

    return base.TakeDamage(damage, elementalDamage, elementType, damageSource);
  }

    protected override void Die()
    {
        base.Die();
        Instantiate(enemyDeathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
