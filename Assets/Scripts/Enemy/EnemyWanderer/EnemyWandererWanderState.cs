using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandererWanderState : EnemyState
{
    private Vector2 moveDirection;
    private EnemyWandererBrain specificEnemyBrain;


    public EnemyWandererWanderState(EnemyBrain enemyBrainBase, EnemyStateMachine stateMachine, string animBoolName, EnemyWandererBrain specificEnemyBrain) 
        : base(stateMachine, enemyBrainBase, animBoolName)
    {
        this.specificEnemyBrain = specificEnemyBrain;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = specificEnemyBrain.RandomWanderTime;

        // Pick a random cardinal direction
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        moveDirection = directions[Random.Range(0, directions.Length)];

        specificEnemyBrain.SetFacingDirection(moveDirection);
        Debug.Log("Moving in direction: " + moveDirection);
    }

    public override void Exit()
    {
         base.Exit();
         Debug.Log("Exiting Move State");
    }   
    public override void Update()
    {
        base.Update();
        // Move enemy
        Move();
        if (stateTimer < 0)
        {
            specificEnemyBrain.StopMovement();
            stateMachine.ChangeState(specificEnemyBrain.idleState);
        }
    }
        public void Move()
    {
        if(specificEnemyBrain.isKnockbacked)
            return;
   
        specificEnemyBrain.rb.velocity = moveDirection * specificEnemyBrain.MoveSpeed;
        specificEnemyBrain.attackCheck.localPosition = moveDirection * specificEnemyBrain.attackDistanceOffset; // distance from center

        specificEnemyBrain.animator.SetFloat("MoveX", moveDirection.x);
        specificEnemyBrain.animator.SetFloat("MoveY", moveDirection.y);
    }
}
