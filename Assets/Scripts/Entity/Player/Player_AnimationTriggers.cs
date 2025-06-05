using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationTriggers : MonoBehaviour
{
    private Player_Brain playerBrain => GetComponentInParent<Player_Brain>();

    private void CallAnimationFinishTrigger()
    {
        playerBrain.CallAnimationFinishTrigger();
    }

    private void OnAnimationAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerBrain.meleeAttackCheck.position, playerBrain.attackCheckRange);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                if (collider.TryGetComponent(out Enemy_Brain enemyBrain))
                {
                    if (enemyBrain.TryGetComponent(out Entity_Stats enemyEntitytats))
                    {
                       bool wasHit = playerBrain.entityStats.DoDamage(enemyEntitytats);
                        if (wasHit)
                        {
                            enemyBrain.TakeDamageEffect(playerBrain.transform.position, playerBrain.GetKnockbackForce(), playerBrain.GetKnockbackDuration());
                        }
                    }
                }
            }
        }
    }
}
