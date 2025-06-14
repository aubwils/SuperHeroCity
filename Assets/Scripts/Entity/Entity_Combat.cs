using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_Stats entityStats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float taragetCheckRadious = 1f;
    [SerializeField] private LayerMask targetLayerMask;

    private void Awake()
    {
        entityStats = GetComponent<Entity_Stats>();
        
    }

    public virtual void PerformAttack()
    {
        foreach (var collider in GetDetectedColliders())
        {

            if (collider.TryGetComponent(out IDamageable damageable))
            {

                float damage = entityStats.GetPhisicalDamage(out bool isCrit);
                damageable.TakeDamage(damage, transform);
                if (TryGetComponent(out Entity_VFX vfx))
                {
                    Vector2 facingDir = GetComponent<Entity_Brain>().GetFacingDirection(); ;
                    vfx.PlayHitVFX(facingDir, isCrit);
                }
            }
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, taragetCheckRadious, targetLayerMask);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetCheck.position, taragetCheckRadious);
    }
}
