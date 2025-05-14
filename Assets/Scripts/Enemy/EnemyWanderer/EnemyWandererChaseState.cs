using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandererChaseState : EnemyState
{
    private EnemyWandererBrain specificEnemyBrain;

    public EnemyWandererChaseState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemyWandererBrain specificEnemyBrain) 
        : base(stateMachine, enemyBrainBase, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }

    
    public override void Enter()
    {
        base.Enter();
        specificEnemyBrain.effectIcons.ShowAlertEffectIcon();
        Debug.Log("Chasing Player");
    }

    public override void Exit()
    {
        base.Exit();
        specificEnemyBrain.effectIcons.HideEffectIcon();
        Debug.Log("Exiting Chase State");
    }

    public override void Update()
    {
        base.Update();
        if (specificEnemyBrain.IsPlayerInAttackRange())
        {
            stateMachine.ChangeState(specificEnemyBrain.attackState);
            return;
        }

        if (!specificEnemyBrain.IsPlayerInSight())
        {
            stateMachine.ChangeState(specificEnemyBrain.wanderState);
            return;
        }

        ChasePlayer();


       
    }

    private void ChasePlayer()
    {
        if (specificEnemyBrain.isKnockbacked)
            return;

        if (specificEnemyBrain.PlayerTarget == null)
            return;

        MoveToward(specificEnemyBrain.PlayerTarget.position);
    }
       private void MoveToward(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)specificEnemyBrain.transform.position).normalized;

        specificEnemyBrain.SetFacingDirection(direction);
        specificEnemyBrain.rb.velocity = direction * specificEnemyBrain.ChaseSpeed;
        specificEnemyBrain.attackCheck.localPosition = direction * specificEnemyBrain.attackDistanceOffset; // distance from center
        specificEnemyBrain.animator.SetFloat("MoveX", direction.x);
        specificEnemyBrain.animator.SetFloat("MoveY", direction.y);
    }

}
