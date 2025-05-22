using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererIdleState : EnemySimpleWandererBaseState
{

     public EnemySimpleWandererIdleState(EnemyBrain enemyBrain, StateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName, specificEnemyBrain)
    {
  
    }

   public override void Enter()
   {
       base.Enter();
       specificEnemyBrain.rb.velocity = Vector2.zero;
       stateTimer = specificEnemyBrain.IdleDuration;;
       //Debug.Log("Idle");
   }
    public override void Exit()
    {
         base.Exit();
        // Debug.Log("Exiting Idle State");
    }   
    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(specificEnemyBrain.moveState);
        }
    }


}
