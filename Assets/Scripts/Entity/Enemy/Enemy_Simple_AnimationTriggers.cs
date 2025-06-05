using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple_AnimationTriggers : MonoBehaviour
{
  private Enemy_Brain enemyBrain => GetComponentInParent<Enemy_Brain>();
//can i do enity brain? then only have 1 animation trigger scrit? or a base?
  private void CallAnimationFinishTrigger()
  {
    enemyBrain.CallAnimationFinishTrigger();
  }

  private void OnAnimationAttackTrigger()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(enemyBrain.meleeAttackCheck.position, enemyBrain.attackCheckRange);
    foreach (var collider in colliders)
    {
      if (collider.TryGetComponent(out IDamageable damageable))
      {
        if (collider.TryGetComponent(out Player_Brain playerBrain))
        {
          if (playerBrain.TryGetComponent(out Entity_Stats playerEntityStats))
          {
            bool wasHit = enemyBrain.entityStats.DoDamage(playerEntityStats);
            if (wasHit)
            {
              playerBrain.TakeDamageEffect(enemyBrain.transform.position, enemyBrain.GetKnockbackForce(), enemyBrain.GetKnockbackDuration());
            }
          }
          // collider.GetComponent<Entity_Stats>().TakeDamage(enemyBrain.entityStats.damage.GetValue());

        }
      }
    }
  }
}
