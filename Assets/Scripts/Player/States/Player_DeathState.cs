using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DeathState : PlayerState
{
    public Player_DeathState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName) : base(playerBrain, stateMachine,  animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       // player.playerMovement.SetCanMove(false);
        

    }

    public override void Update()
    {
        base.Update();
        
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void CallAnimationFinishTrigger()
    {
        base.CallAnimationFinishTrigger();
    }


}
