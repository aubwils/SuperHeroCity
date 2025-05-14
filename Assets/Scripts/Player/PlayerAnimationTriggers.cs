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
        if (collider.GetComponent<EnemyBrain>() !=null)
        collider.GetComponent<EnemyBrain>().TakeDamage();
        }
    }
}
