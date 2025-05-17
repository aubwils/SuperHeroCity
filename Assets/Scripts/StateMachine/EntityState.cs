using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine stateMachine;
    protected string animBoolName;
    
    protected Animator animator;
    protected bool animationTriggerCalled;

    protected float stateTimer;


    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

    }

    public virtual void Enter()
    {
        animator.SetBool(animBoolName, true);
        animationTriggerCalled = false;
        //Debug.Log($"Entering {stateName} state");
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        // Debug.Log($"Updating {stateName} state");
    }

    public virtual void Exit()
    {
        animator.SetBool(animBoolName, false);
        // Debug.Log($"Exiting {stateName} state");
    }

     public virtual void FixedUpdate() 
    {
     
    }

    public virtual void CallAnimationFinishTrigger() 
    {
        animationTriggerCalled = true;
    }
}
