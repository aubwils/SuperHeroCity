using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple_Wanderer_BaseState : EnemyState
{
    protected Enemy_Simple_Wanderer_Brain specificEnemyBrain;

    public Enemy_Simple_Wanderer_BaseState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName, Enemy_Simple_Wanderer_Brain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }

    public override void Enter()
    {
        base.Enter();

    }
    public override void Exit()
    {
        base.Exit();

    }
    public override void Update()
    {
        base.Update();
        if (specificEnemyBrain.IsPlayerInSight())
        {
            stateMachine.ChangeState(specificEnemyBrain.chaseState);
        }  
    }
}
