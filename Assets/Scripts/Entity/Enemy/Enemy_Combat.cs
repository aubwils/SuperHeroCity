using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : Entity_Combat
{
   [Header("Enemy Attack Cooldown")]
    [SerializeField] private float attackCooldown = 1.5f; // or whatever makes sense
    private float lastAttackTime;

    public override void PerformAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        
        lastAttackTime = Time.time;

        base.PerformAttack(); // Calls the shared damage logic
    }
}
