using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererChaseState : EnemyState
{
    private EnemySimpleWandererBrain specificEnemyBrain;

    public EnemySimpleWandererChaseState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain enemyBrain) : base(stateMachine, enemyBrain, animBoolName)
   {
        this.specificEnemyBrain = enemyBrain;
   }

    public override void Enter()
    {
        base.Enter();
        specificEnemyBrain.effectIcons.ShowAlertEffectIcon();
        specificEnemyBrain.currentMoveSpeed = specificEnemyBrain.ChaseSpeed;
        Debug.Log("Chasing Player");
    }

    public override void Exit()
    {
        base.Exit();
        specificEnemyBrain.effectIcons.HideEffectIcon();
        specificEnemyBrain.currentMoveSpeed = specificEnemyBrain.MoveSpeed;
        Debug.Log("Exiting Chase State");
    }

    public override void Update()
    {
        base.Update();
        if (specificEnemyBrain.IsPlayerInAttackRange() && CanAttack())
        {
            stateMachine.ChangeState(specificEnemyBrain.meleeAttackState);
            return;
        }  
        if (specificEnemyBrain.PlayerTarget != null && specificEnemyBrain.IsPlayerInSight()){
            ChasePlayer();
        }
        else
        {
            specificEnemyBrain.TimeSinceLastSeen += Time.deltaTime;
        }
        ChaseLastKnownPosition();
    }

    private void ChasePlayer()
    {
        if (specificEnemyBrain.PlayerTarget == null)
            return;

        MoveToward(specificEnemyBrain.PlayerTarget.position);
    }

    private void ChaseLastKnownPosition()
    {
        if (!specificEnemyBrain.lastKnownPlayerPosition.HasValue)
            stateMachine.ChangeState(specificEnemyBrain.confusedState);
           

        Vector2 playerLastKnownPosition = specificEnemyBrain.lastKnownPlayerPosition.Value;
        MoveToward(playerLastKnownPosition);

        float distance = Vector2.Distance(specificEnemyBrain.transform.position, playerLastKnownPosition);
        if (distance < 0.1f)
        {
            specificEnemyBrain.lastKnownPlayerPosition = null;
            specificEnemyBrain.rb.velocity = Vector2.zero; 
            Debug.Log("Reached last known position, lost player");
            stateMachine.ChangeState(specificEnemyBrain.confusedState);
        }
    }

    private void MoveToward(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)specificEnemyBrain.transform.position).normalized;

        specificEnemyBrain.SetFacingDirection(direction);
        specificEnemyBrain.rb.velocity = direction * specificEnemyBrain.currentMoveSpeed;
        specificEnemyBrain.attackCheck.localPosition = direction * specificEnemyBrain.attackDistanceOffset; // distance from center
        specificEnemyBrain.animator.SetFloat("MoveX", direction.x);
        specificEnemyBrain.animator.SetFloat("MoveY", direction.y);
    }
  
    private bool CanAttack()
    {
        if(Time.time >= specificEnemyBrain.lastAttackTime + specificEnemyBrain.attackCooldown)
        {
            specificEnemyBrain.lastAttackTime = Time.time;
            return true;
        }

        return false;
    }

}
