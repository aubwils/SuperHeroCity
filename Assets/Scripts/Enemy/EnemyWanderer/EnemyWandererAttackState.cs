using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandererAttackState : EnemyState
{
    private EnemyWandererBrain specificEnemyBrain;

    public EnemyWandererAttackState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemyWandererBrain specificEnemyBrain) 
        : base(stateMachine, enemyBrainBase, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }
    public override void Enter()
    {
        base.Enter();
        specificEnemyBrain.StopMovement();
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
       
    }
}
