using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmarterWandererRecoveryState : EnemyState
{
  private EnemySmarterWandererBrain specificEnemyBrain;

    public EnemySmarterWandererRecoveryState(EnemyBrain enemyBrain, StateMachine stateMachine, string animBoolName, EnemySmarterWandererBrain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = specificEnemyBrain.timeBetweenAttacks;
        //Debug.Log("Entering Recovery State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(specificEnemyBrain.idleState);
        }
    }
}
