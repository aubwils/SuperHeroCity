using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererRecoveryState : EnemyState
{
    private EnemySimpleWandererBrain specificEnemyBrain;

    public EnemySimpleWandererRecoveryState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain enemyBrain) : base(stateMachine, enemyBrain, animBoolName)
   {
        this.specificEnemyBrain = enemyBrain;
   }

    public override void Enter()
    {
        base.Enter();
        stateTimer = specificEnemyBrain.timeBetweenAttacks;
        Debug.Log("Entering Recovery State");
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
