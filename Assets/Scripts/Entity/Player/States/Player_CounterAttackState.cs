using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat playerCombat;
    private bool counteredSomebody;

    public Player_CounterAttackState(Player_Brain playerBrain, StateMachine stateMachine, string animBoolName) : base(playerBrain, stateMachine, animBoolName)
    {
        playerCombat = playerBrain.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();
        counteredSomebody = playerCombat.CounterAttackPreformed();
        stateTimer = playerCombat.GetCounterRecoveryDuration();

        playerBrain.animator.SetBool("CounterAttackPerformed", counteredSomebody);
        
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Counter Attack State Exited");
    }
    public override void Update()
    {
        base.Update();
        
        if (animationTriggerCalled)
            stateMachine.ChangeState(playerBrain.idleState);
        
        if (stateTimer < 0 && !counteredSomebody)
            stateMachine.ChangeState(playerBrain.idleState);
   

    }
}
