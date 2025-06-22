using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : Entity_Health
{
    private Player_Brain playerBrain => GetComponent<Player_Brain>();


    public override bool TakeDamage(float damage, float elementalDamage, ElementType elementType, Transform damageSource)
    {
        return base.TakeDamage(damage, elementalDamage, elementType, damageSource);
    }

    protected override void Die()
    {
        base.Die();
        playerBrain.StateMachine.ChangeState(playerBrain.deathState);
    }
}
