using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, StateMachine stateMachine,  string animBoolName) : base(player, stateMachine,  animBoolName)
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
