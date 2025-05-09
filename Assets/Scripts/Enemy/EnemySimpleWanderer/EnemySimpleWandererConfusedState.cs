using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleWandererConfusedState : EnemySimpleWandererIdleState 
{

    public EnemySimpleWandererConfusedState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemySimpleWandererBrain enemyBrain) : base(enemyBrainBase, stateMachine, animBoolName, enemyBrain)
    {
        
    }
    
    public override void Enter()
    {
        base.Enter();
        stateTimer = 1.5f;
        specificEnemyBrain.rb.velocity = Vector2.zero;
        specificEnemyBrain.effectIcons.ShowConfusedEffectIcon();
    }
        public override void Exit()
        {
            base.Exit();
            specificEnemyBrain.effectIcons.HideEffectIcon();
        }   
        public override void Update()
        {
            base.Update();
            if(stateTimer < 0)
            {
                stateMachine.ChangeState(specificEnemyBrain.moveState);
            }
        }


}