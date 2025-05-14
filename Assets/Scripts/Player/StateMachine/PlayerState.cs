using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    protected bool animationTriggerCalled;

    public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
    }
    
    public virtual void Enter() 
    {
        // Debug.Log("Entering state: " + animBoolName);
        player.animator.SetBool(animBoolName, true);
        animationTriggerCalled = false;
        
    }
    
    public virtual void Exit() 
    {
        // Debug.Log("Exiting state: " + animBoolName);
        player.animator.SetBool(animBoolName, false);
    }
    
    public virtual void Update() 
    {
         if (player.isKnockbacked) return;

    }

    public virtual void FixedUpdate() 
    {
     
    }

    public virtual void AnimationFinishTrigger() 
    {
        animationTriggerCalled = true;
    }
    
}
