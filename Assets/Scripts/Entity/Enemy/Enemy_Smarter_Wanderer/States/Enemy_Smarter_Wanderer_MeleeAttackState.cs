using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Smarter_Wanderer_MeleeAttackState : EnemyState
{
    private Enemy_Smarter_Wanderer_Brain specificEnemyBrain;


    public Enemy_Smarter_Wanderer_MeleeAttackState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName, Enemy_Smarter_Wanderer_Brain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }

    public override void Enter()
    {
        base.Enter();
        specificEnemyBrain.rb.velocity = Vector2.zero;
        //Debug.Log("Attacking Player");
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exiting Attack State");
    }

    public override void Update()
    {
        base.Update();
        if (animationTriggerCalled)
        {
            if (specificEnemyBrain.IsPlayerInAttackRange())
            {
                // Player still in range, attack again
                stateMachine.ChangeState(specificEnemyBrain.meleeAttackState);
            }
            else if (specificEnemyBrain.IsPlayerInSight())
            {
                // Player knocked back but still visible, chase
                stateMachine.ChangeState(specificEnemyBrain.chaseState);
            }
            else
            {
                // Lost sight of player
                stateMachine.ChangeState(specificEnemyBrain.idleState);
            }
        }  
    }
}
