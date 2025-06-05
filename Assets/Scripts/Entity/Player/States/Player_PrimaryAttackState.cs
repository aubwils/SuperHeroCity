using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;


     
      public Player_PrimaryAttackState(Player_Brain playerBrain, StateMachine stateMachine,  string animBoolName) : base(playerBrain, stateMachine,  animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        
        playerBrain.animator.SetInteger("ComboCounter", comboCounter);

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

        playerBrain.StartCoroutine(playerBrain.BusyFor(0.15f));



        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if(animationTriggerCalled)
        {
            stateMachine.ChangeState(playerBrain.idleState);
        }
}
}

