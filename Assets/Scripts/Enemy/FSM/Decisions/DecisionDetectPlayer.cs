using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDetectPlayer : EnemyFSMDecision
{
    [Header("Detect Player Settings")]
    [SerializeField] private float detectionRangeRadius = 5f;
    [SerializeField] private LayerMask playerLayer;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return DetectPlayer();
    }

    private bool DetectPlayer()
    {
        Collider2D detectedPlayer = Physics2D.OverlapCircle(enemyBrain.transform.position, detectionRangeRadius, playerLayer);
        if (detectedPlayer != null)
        {
            Debug.Log($"Player detected: {detectedPlayer.name}");
            enemyBrain.SetPlayerTarget(detectedPlayer.transform);
            return true;
        }

        enemyBrain.SetPlayerTarget(null);
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRangeRadius);
    }
}
