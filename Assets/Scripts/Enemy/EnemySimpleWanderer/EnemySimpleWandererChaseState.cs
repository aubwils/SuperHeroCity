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
        specificEnemyBrain.currentMoveSpeed = specificEnemyBrain.ChaseSpeed;
        Debug.Log("Chasing Player");
    }

    public override void Exit()
    {
        base.Exit();
        specificEnemyBrain.currentMoveSpeed = specificEnemyBrain.MoveSpeed;
        Debug.Log("Exiting Chase State");
    }

    public override void Update()
    {
        base.Update();
        if (specificEnemyBrain.IsPlayerInAttackRange())
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
            stateMachine.ChangeState(specificEnemyBrain.idleState);

        Vector2 targetPos = specificEnemyBrain.lastKnownPlayerPosition.Value;
        MoveToward(targetPos);

        float distance = Vector2.Distance(specificEnemyBrain.transform.position, targetPos);
        if (distance < 0.1f)
        {
            specificEnemyBrain.lastKnownPlayerPosition = null;
            specificEnemyBrain.rb.velocity = Vector2.zero; // stop after reaching it
            Debug.Log("Reached last known position, lost player");
            stateMachine.ChangeState(specificEnemyBrain.idleState);
        }
    }

    private void MoveToward(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)specificEnemyBrain.transform.position).normalized;

        specificEnemyBrain.SetFacingDirection(direction);
        specificEnemyBrain.rb.velocity = direction * specificEnemyBrain.currentMoveSpeed;
        specificEnemyBrain.animator.SetFloat("MoveX", direction.x);
        specificEnemyBrain.animator.SetFloat("MoveY", direction.y);
    }


}
