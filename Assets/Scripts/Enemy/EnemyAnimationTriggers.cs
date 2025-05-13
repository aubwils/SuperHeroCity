using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    private EnemyBrain enemyBrain => GetComponentInParent<EnemyBrain>();

    private void AnimationTrigger()
    {
        enemyBrain.AnimationTrigger();
    }
   private void AnimationTriggerAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemyBrain.attackCheck.position, enemyBrain.attackCheckRadius);
        foreach (Collider2D hit in colliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(enemyBrain.attackDamage);
            }
        }
    }


}
