using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//thinking for all enemies if all have idle, chase, attack states then made a enemy base idle, chase, attack states and inherit from it?
public class Enemy_Simple_Wanderer_ChaseState : EnemyState
{
    private Enemy_Simple_Wanderer_Brain specificEnemyBrain;

    public Enemy_Simple_Wanderer_ChaseState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName, Enemy_Simple_Wanderer_Brain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }
    public override void Enter()
    {
        base.Enter();
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
            return;

        if (specificEnemyBrain.GetKnockbackStatus()) return;

        Vector2 direction = (specificEnemyBrain.PlayerTarget.position - specificEnemyBrain.transform.position).normalized;
        specificEnemyBrain.SetFacingDirection(direction);

        specificEnemyBrain.rb.velocity = direction * specificEnemyBrain.currentMoveSpeed;

        specificEnemyBrain.animator.SetFloat("MoveX", direction.x);
        specificEnemyBrain.animator.SetFloat("MoveY", direction.y);
    }
}
