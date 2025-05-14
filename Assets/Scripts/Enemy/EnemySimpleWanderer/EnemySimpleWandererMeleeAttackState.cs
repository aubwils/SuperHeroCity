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
        specificEnemyBrain.StopMovement();
        Debug.Log("Attacking Player");
    }

    public override void Exit()
    {
        base.Exit();
        specificEnemyBrain.lastAttackTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (!specificEnemyBrain.IsPlayerInAttackRange())
        {
            stateMachine.ChangeState(specificEnemyBrain.chaseState);
        }  
    }
}
