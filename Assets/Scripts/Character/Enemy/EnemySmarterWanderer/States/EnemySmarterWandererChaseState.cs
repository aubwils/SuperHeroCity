using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmarterWandererChaseState : EnemyState
{
    private EnemySmarterWandererBrain specificEnemyBrain;

    public EnemySmarterWandererChaseState(EnemyBrain enemyBrain, StateMachine stateMachine, string animBoolName, EnemySmarterWandererBrain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }
    public override void Enter()
    {
        base.Enter();
            Debug.Log("Entered CHASE state!");

        specificEnemyBrain.currentMoveSpeed = specificEnemyBrain.ChaseSpeed;

        if (specificEnemyBrain.PlayerTarget == null)
            specificEnemyBrain.SetPlayerTarget(specificEnemyBrain.GetPlayerReference());

       // Debug.Log("Chasing Player");
    }

    public override void Exit()
    {
        base.Exit();
        specificEnemyBrain.currentMoveSpeed = specificEnemyBrain.MoveSpeed;
        //Debug.Log("Exiting Chase State");
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
            {
                Debug.LogWarning("ChasePlayer called but PlayerTarget is null!");
                return;
            }

        Vector2 direction = (specificEnemyBrain.PlayerTarget.position - specificEnemyBrain.transform.position).normalized;
        specificEnemyBrain.SetFacingDirection(direction);

        specificEnemyBrain.rb.velocity = direction * specificEnemyBrain.currentMoveSpeed;

        specificEnemyBrain.animator.SetFloat("MoveX", direction.x);
        specificEnemyBrain.animator.SetFloat("MoveY", direction.y);
    }
}
