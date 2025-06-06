using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : Entity_Health
{
  private Enemy_Brain enemyBrain => GetComponent<Enemy_Brain>();
  [SerializeField] private GameObject enemyDeathEffectPrefab;

  public override bool TakeDamage(float damage, Transform damageSource)
  {

  if (damageSource.GetComponent<Player_Brain>() != null)
  {
      Debug.Log("Enemy hit by player, attempting to chase!");
      enemyBrain.TryEnterChaseState(damageSource);
  }
  else
  {
      Debug.LogWarning("damageSource is not the player");
  }

    return base.TakeDamage(damage, damageSource);
  }

    protected override void Die()
    {
        base.Die();
        Instantiate(enemyDeathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
