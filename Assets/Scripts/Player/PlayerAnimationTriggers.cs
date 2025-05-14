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

    private void OnAnimationAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackCheck.position, player.attackCheckRange);
        foreach (var collider in colliders)
        {

            if (collider.TryGetComponent(out EnemyBrain enemy))
            {
                Vector2 knockbackDirection = player.playerMovement.GetLastMovementDirection();
                enemy.TakeDamage(player.transform.position, player.GetKnockbackForce(), player.GetKnockbackDuration());
                collider.GetComponent<CharacterStats>().TakeDamage(player.characterStats.damage); 
            }
        }
    }
}
