using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple_Wanderer_MeleeAttackState : EnemyState
{
    private Enemy_Simple_Wanderer_Brain specificEnemyBrain;

    public Enemy_Simple_Wanderer_MeleeAttackState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName, Enemy_Simple_Wanderer_Brain specificEnemyBrain)
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
            stateMachine.ChangeState(specificEnemyBrain.recoveryState); //might nto need once have invincibility...
        }  
    }
}
