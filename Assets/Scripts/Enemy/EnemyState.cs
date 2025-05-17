using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EntityState
{
    protected EnemyBrain enemyBrain;

    public EnemyState(EnemyBrain enemyBrain, StateMachine stateMachine,  string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemyBrain = enemyBrain;

        animator = enemyBrain.animator;
    }
    
    public override void Enter() 
    {
        base.Enter();
    }
    
    public override void Update() 
    {
        base.Update();  
        if (enemyBrain.isKnockbacked) return;
    }
    
    public override void Exit() 
    {
        base.Exit();
    }
    

    
}
