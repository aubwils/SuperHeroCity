using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererBaseState : EnemyState
{
    protected EnemySimpleWandererBrain specificEnemyBrain;

    public EnemySimpleWandererBaseState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain enemyBrain) : base(stateMachine, enemyBrain, animBoolName)
   {
        this.specificEnemyBrain = enemyBrain;
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
