using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererMeleeAttackState : EnemyState
{
    private EnemySimpleWandererBrain specificEnemyBrain;

    public EnemySimpleWandererMeleeAttackState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain enemyBrain) : base(stateMachine, enemyBrain, animBoolName)
   {
        this.specificEnemyBrain = enemyBrain;
   }

    public override void Enter()
    {
        base.Enter();
        specificEnemyBrain.rb.velocity = Vector2.zero;
        Debug.Log("Attacking Player");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Attack State");
    }

    public override void Update()
    {
        base.Update();
        if (animationTriggerCalled)
        {
            stateMachine.ChangeState(specificEnemyBrain.recoveryState);
        }  
    }
}
