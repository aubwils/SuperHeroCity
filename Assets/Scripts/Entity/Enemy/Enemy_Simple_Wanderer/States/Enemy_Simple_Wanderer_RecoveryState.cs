using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple_Wanderer_RecoveryState : EnemyState
{
    private Enemy_Simple_Wanderer_Brain specificEnemyBrain;

    public Enemy_Simple_Wanderer_RecoveryState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName, Enemy_Simple_Wanderer_Brain specificEnemyBrain)
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
