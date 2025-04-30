using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChase : EnemyFSMAction
{
    [Header("Chase Settings")]
    [SerializeField] private float chaseSpeed = 2f;
    [SerializeField] private float chaseDistance = 1.3f;
    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override void Act()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (enemyBrain.PlayerTarget == null) return;
        
        Vector3 directionToPlayer = enemyBrain.PlayerTarget.position - transform.position;
        if(directionToPlayer.magnitude > chaseDistance)
        {
            transform.Translate(directionToPlayer.normalized * (chaseSpeed * Time.deltaTime));
        }
        
    }   

}
