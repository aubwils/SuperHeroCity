using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Smarter_Wanderer_StunnedState : EnemyState
{

    private Enemy_Smarter_Wanderer_Brain specificEnemyBrain;

    private Enemy_VFX enemyVFX;

    public Enemy_Smarter_Wanderer_StunnedState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName, Enemy_Smarter_Wanderer_Brain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
        enemyVFX = specificEnemyBrain.GetComponent<Enemy_VFX>();

    }

    public override void Enter()
    {
        base.Enter();
        enemyVFX.EnableAttackAlert(false);
        specificEnemyBrain.EnableCounterWindow(false);
        stateTimer = specificEnemyBrain.stunnedDuration;

        ApplyStunKnockback();
    }

    public override void Update()
    {
        base.Update();

        // Check if the stun duration has passed
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(specificEnemyBrain.idleState);
        }
    }

    private void ApplyStunKnockback()
    {
        // Get direction away from player (assuming player stunned the enemy)
        if (specificEnemyBrain.PlayerTarget != null)
        {
            Vector2 direction = (specificEnemyBrain.transform.position - specificEnemyBrain.PlayerTarget.position).normalized;
            Vector2 knockback = direction * specificEnemyBrain.stunnedVelocity.magnitude;

            specificEnemyBrain.ReciveKnockback(knockback, specificEnemyBrain.stunnedDuration);
        }
        else
        {
            // Fallback: knockback in facing direction
            Vector2 knockback = specificEnemyBrain.FacingDirection * specificEnemyBrain.stunnedVelocity.magnitude;
            specificEnemyBrain.ReciveKnockback(knockback, specificEnemyBrain.stunnedDuration);
        }
    }

}
