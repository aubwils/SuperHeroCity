using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionPlayerInAttackRange : EnemyFSMDecision
{
    [Header("Detect Player Settings")]
    [SerializeField] private float detectionAttackRangeRadius = 5f;
    [SerializeField] private LayerMask playerLayer;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public override bool Decide()
    {
       return PlayerInAttackRange();
    }

    private bool PlayerInAttackRange()
    {
        if(enemyBrain.PlayerTarget == null) return false;

        Collider2D detectedPlayers = Physics2D.OverlapCircle(enemyBrain.transform.position, detectionAttackRangeRadius, playerLayer);

        if (detectedPlayers != null)
        {
            Debug.Log($"Player detected: {detectedPlayers.name}");
            return true;
        }

        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionAttackRangeRadius);
    }

}
