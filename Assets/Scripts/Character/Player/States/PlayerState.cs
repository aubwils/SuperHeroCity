using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    //protected PlayerInputActions playerInputActions; 



    public PlayerState(Player player, StateMachine stateMachine,  string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;
        //playerInputActions = player.playerInputActions;
    }

    public override void Enter()
    {
        base.Enter();
        player.CurrentAnimator.SetBool(animBoolName, true);

    }

    public override void Update() 
    {
        base.Update();
         if (player.isKnockbacked) return;
    }   

    public override void FixedUpdate() 
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        player.CurrentAnimator.SetBool(animBoolName, false);

    }
    

    
}
