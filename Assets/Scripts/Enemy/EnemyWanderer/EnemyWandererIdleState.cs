using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandererIdleState : EnemyState
{
    private EnemyWandererBrain specificEnemyBrain;

    public EnemyWandererIdleState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemyWandererBrain specificEnemyBrain) 
        : base(stateMachine, enemyBrainBase, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }

    public override void Enter()
    {
        base.Enter();
        specificEnemyBrain.StopMovement();
        stateTimer = specificEnemyBrain.IdleDuration;
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
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(specificEnemyBrain.wanderState);
        }
    }
}
