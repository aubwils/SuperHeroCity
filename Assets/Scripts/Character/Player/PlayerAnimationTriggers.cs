using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void CallAnimationFinishTrigger()
    {
        player.CallAnimationFinishTrigger();
    }

    private void OnAnimationAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.meleeAttackCheck.position, player.attackCheckRange);
        foreach (var collider in colliders)
        {

            if (collider.TryGetComponent(out EnemyBrain enemy))
            {    
                 if (enemy.TryGetComponent(out CharacterStats enemyCharacterStats))
                {
                    player.characterStats.DoDamage(enemyCharacterStats);
                    Vector2 knockbackDirection = player.playerMovement.GetLastMovementDirection();
                    enemy.TakeDamageEffect(player.transform.position, player.GetKnockbackForce(), player.GetKnockbackDuration());
                }
                
                
            }
        }
    }
}
