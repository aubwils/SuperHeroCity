using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererIdleState : EnemyState
{
    private EnemySimpleWandererBrain specificEnemyBrain;

   public EnemySimpleWandererIdleState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain enemyBrain) : base(stateMachine, enemyBrain, animBoolName)
   {
        this.specificEnemyBrain = enemyBrain;
   }

   public override void Enter()
   {
       base.Enter();
       stateTimer = specificEnemyBrain.IdleDuration;;
       Debug.Log("Idle");
   }
    public override void Exit()
    {
         base.Exit();
         Debug.Log("Exiting Idle State");
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
