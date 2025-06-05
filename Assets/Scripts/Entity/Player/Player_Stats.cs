using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : Entity_Stats
{   
    private Player_Brain playerBrain;

    protected override void Start()
    {
        base.Start();
        playerBrain = GetComponent<Player_Brain>();
        
    }

    public override bool TakeDamage(int damage)
    {
       return base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        playerBrain.StateMachine.ChangeState(playerBrain.deathState);
    }
}