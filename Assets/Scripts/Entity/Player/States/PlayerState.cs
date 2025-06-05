using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player_Brain playerBrain;
    //protected PlayerInputActions playerInputActions; 



    public PlayerState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName) : base(stateMachine, animBoolName)
    {
        this.playerBrain = playerBrain;
        //playerInputActions = player.playerInputActions;
    }

    public override void Enter()
    {
        base.Enter();
        playerBrain.CurrentAnimator.SetBool(animBoolName, true);

    }

    public override void Update() 
    {
        base.Update();
         if (playerBrain.isKnockbacked) return;
    }   

    public override void FixedUpdate() 
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        playerBrain.CurrentAnimator.SetBool(animBoolName, false);

    }
    

    
}
