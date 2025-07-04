using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Smarter_Wanderer_MoveState : Enemy_Smarter_Wanderer_BaseState
{
   private Vector2 moveDirection;

    public Enemy_Smarter_Wanderer_MoveState(Enemy_Brain enemyBrain, StateMachine stateMachine, string animBoolName, Enemy_Smarter_Wanderer_Brain specificEnemyBrain)
        : base(enemyBrain, stateMachine, animBoolName, specificEnemyBrain)
    {
   
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = specificEnemyBrain.RandomWanderTime;

        // Pick a random cardinal direction
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        moveDirection = directions[Random.Range(0, directions.Length)];

        specificEnemyBrain.SetFacingDirection(moveDirection);
       // Debug.Log("Moving in direction: " + moveDirection);
    }
    public override void Exit()
    {
         base.Exit();
       //  Debug.Log("Exiting Move State");
    }   
    public override void Update()
    {
        base.Update();
        // Move enemy
        Move();
        if (stateTimer < 0)
        {
            specificEnemyBrain.rb.velocity = Vector2.zero;
            stateMachine.ChangeState(specificEnemyBrain.idleState);
        }
    }
        public void Move()
    {
        specificEnemyBrain.rb.velocity = moveDirection * specificEnemyBrain.currentMoveSpeed;
        specificEnemyBrain.animator.SetFloat("MoveX", moveDirection.x);
        specificEnemyBrain.animator.SetFloat("MoveY", moveDirection.y);
    }
    
}
