using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AnimationTriggerAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (Collider2D hit in colliders)
        {
            EnemyBrain enemy = hit.GetComponent<EnemyBrain>();
            if (enemy != null)
            {
                enemy.TakeDamage(player.attackDamage);
            }
        }
    }
}
