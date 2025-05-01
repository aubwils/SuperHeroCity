using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionAttackRange : EnemyFSMDecision
{
    [Header("Detect Player Settings")]
    [SerializeField] private float attackRangeRadius = 2f;
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
        Collider2D detectedPlayer = Physics2D.OverlapCircle(enemyBrain.transform.position, attackRangeRadius, playerLayer);
        if (detectedPlayer != null)
        {
            Debug.Log($"Player in Attack Range detected: {detectedPlayer.name}");
            return true;
        }

        enemyBrain.SetPlayerTarget(null);
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRangeRadius);
    }


}
