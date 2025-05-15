using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleAnimationTriggers : MonoBehaviour
{
  private EnemyBrain enemyBrain => GetComponentInParent<EnemyBrain>();

  private void OnAnimationFinishTrigger()
  {
    enemyBrain.OnAnimationFinishTrigger();
  }

  private void OnAnimationAttackTrigger()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(enemyBrain.meleeAttackCheck.position, enemyBrain.attackCheckRange);
    foreach (var collider in colliders)
    {
          if (collider.TryGetComponent(out Player player))
          {
                 if (player.TryGetComponent(out CharacterStats playerCharacterStats))
            {
                enemyBrain.characterStats.DoDamage(playerCharacterStats);
                player.TakeDamageEffect(enemyBrain.transform.position, enemyBrain.GetKnockbackForce(), enemyBrain.GetKnockbackDuration());
            }
             // collider.GetComponent<CharacterStats>().TakeDamage(enemyBrain.characterStats.damage.GetValue());
          }
    }
  }
}
