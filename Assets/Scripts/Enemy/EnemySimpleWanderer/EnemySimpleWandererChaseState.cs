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
        if (!specificEnemyBrain.IsPlayerInSight())
        {
            stateMachine.ChangeState(specificEnemyBrain.idleState);
            return;
        }
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (specificEnemyBrain.PlayerTarget == null)
            return;

        Vector2 direction = (specificEnemyBrain.PlayerTarget.position - specificEnemyBrain.transform.position).normalized;
        specificEnemyBrain.SetFacingDirection(direction);

        specificEnemyBrain.rb.velocity = direction * specificEnemyBrain.currentMoveSpeed;

        specificEnemyBrain.animator.SetFloat("MoveX", direction.x);
        specificEnemyBrain.animator.SetFloat("MoveY", direction.y);
    }
}
