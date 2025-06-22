using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy_Brain enemyBrain;
    protected Animator animator;

    public EnemyState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName)
        : base(stateMachine, animBoolName)
    {
        this.enemyBrain = enemyBrain;
        this.animator = enemyBrain.animator;
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(animBoolName, true);
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool(animBoolName, false);
    }
    
}
