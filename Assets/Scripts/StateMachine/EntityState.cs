using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected bool animationTriggerCalled;
    protected float stateTimer;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        animationTriggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
    }

    public virtual void FixedUpdate() 
    {
    }

    public virtual void CallAnimationFinishTrigger() 
    {
        animationTriggerCalled = true;
    }
}