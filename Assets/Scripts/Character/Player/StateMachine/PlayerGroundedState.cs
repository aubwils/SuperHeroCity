using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, StateMachine stateMachine,  string animBoolName) : base(player, stateMachine,  animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        // Temporary testing: Press L to transform
        if (Input.GetKeyDown(KeyCode.L))
        {
            stateMachine.ChangeState(player.transformationState);
        }

    }
}   