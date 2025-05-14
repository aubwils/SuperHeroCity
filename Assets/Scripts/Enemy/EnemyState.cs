using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
  protected EnemyStateMachine stateMachine;
    protected EnemyBrain enemyBrain;

    private string animBoolName;
    protected float stateTimer;

    protected bool animationTriggerCalled;

    public EnemyState(EnemyStateMachine stateMachine, EnemyBrain enemyBrain, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemyBrain = enemyBrain;
        this.animBoolName = animBoolName;
    }
    
    public virtual void Enter() 
    {
        // Debug.Log("Entering state: " + animBoolName);
        enemyBrain.animator.SetBool(animBoolName, true);
        animationTriggerCalled = false;
        
    }
    
    public virtual void Exit() 
    {
        // Debug.Log("Exiting state: " + animBoolName);
        enemyBrain.animator.SetBool(animBoolName, false);
    }
    
    public virtual void Update() 
    {
      stateTimer -= Time.deltaTime;
    }


    public virtual void AnimationFinishTrigger() 
    {
        animationTriggerCalled = true;
    }
    
}
