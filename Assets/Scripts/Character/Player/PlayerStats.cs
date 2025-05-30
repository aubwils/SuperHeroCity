using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{   
    private Player player;

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
        
    }

    public override bool TakeDamage(int damage)
    {
       return base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        player.StateMachine.ChangeState(player.deathState);
    }
}