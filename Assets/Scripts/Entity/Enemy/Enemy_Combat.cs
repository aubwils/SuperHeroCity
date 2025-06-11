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
        {
            Debug.Log($"{gameObject.name} tried to attack but is on cooldown.");
            return;
        }

        lastAttackTime = Time.time;

        Debug.Log($"{gameObject.name} is performing an attack!");
        base.PerformAttack(); // Calls the shared damage logic
    }
}
