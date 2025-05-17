using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererBaseState : EnemyState
{
    protected EnemySimpleWandererBrain specificEnemyBrain;

    public EnemySimpleWandererBaseState(EnemyBrain enemyBrain, StateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain specificEnemyBrain)
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
