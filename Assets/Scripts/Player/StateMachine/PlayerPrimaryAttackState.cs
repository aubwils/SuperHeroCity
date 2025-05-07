using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;


     
      public PlayerPrimaryAttackState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        
        player.animator.SetInteger("ComboCounter", comboCounter);

        // #region Come Back to in Future
        // // Attack Movement Data for pushing the player during attack. Felt weird in testing will look at again once we have real attack animations
        // // Was in Player and Player PrimaryAttackState scripts
        // // Vector2 moveDir = player.playerMovement.GetLastMovementDirection();
        // // Player.AttackMovementData pushData = player.attackMovement[comboCounter];
        // // player.StartCoroutine(player.ApplyAttackPush(moveDir, pushData.pushDistance, pushData.pushDuration));
        // #region EndRegion

    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine(player.BusyFor(0.15f));



        comboCounter++;
        lastTimeAttacked = Time.time;
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

