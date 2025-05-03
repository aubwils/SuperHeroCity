using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
      public PlayerPrimaryAttackState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Primary Attack Enter");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Primary Attack Exit");
    }

    public override void Update()
    {
        base.Update();
        if(animationTriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
}
}

