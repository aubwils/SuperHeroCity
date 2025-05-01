using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttackMelee : EnemyFSMAction
{
    [Header("Melee Attack Settings")]
    private EnemyBrain enemyBrain;
    private Rigidbody2D rb;


    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Act()
    {
        MeleeAttack();
    }

    private void MeleeAttack()
    {
       
    }
}
