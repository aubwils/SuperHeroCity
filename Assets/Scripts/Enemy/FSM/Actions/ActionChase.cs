using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : EnemyFSMAction
{
    [Header("Chase Settings")]
    [SerializeField] private float chaseSpeed = 2f;
    [SerializeField] private float chaseDistance = 1.3f;
    private EnemyBrain enemyBrain;
    private Rigidbody2D rb;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Act()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (enemyBrain.PlayerTarget == null) return;

        Vector3 directionToPlayer = enemyBrain.PlayerTarget.position - transform.position;
        if (directionToPlayer.magnitude > chaseDistance)
        {
            rb.velocity = directionToPlayer.normalized * chaseSpeed;
            Debug.DrawLine(transform.position, enemyBrain.PlayerTarget.position, Color.red);  
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving when within chase distance
        }
    }
}
