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

    [Header("Status Effect Details")]
    [SerializeField] private float defaultDuration = 3f;
    [SerializeField] private float chilledSlowMultiplier = 0.4f;
    


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

                float elementalDamage = entityStats.GetElementalDamage(out ElementType elementType);
                float damage = entityStats.GetPhisicalDamage(out bool isCrit);
                damageable.TakeDamage(damage, elementalDamage, elementType, transform);
                if(elementType != ElementType.None)
                    ApplyStatusEffect(collider.transform, elementType);

                if (TryGetComponent(out Entity_VFX vfx))
                {
                    vfx.UpdateOnHitColorForElementColor(elementType);
                    Vector2 facingDir = GetComponent<Entity_Brain>().GetFacingDirection(); ;
                    vfx.PlayHitVFX(facingDir, isCrit);
                }
            }
        }
    }

    public void ApplyStatusEffect(Transform target, ElementType elementType)
    {
        Entity_StatusHandler entityStatusHandler = target.GetComponent<Entity_StatusHandler>();
        if (entityStatusHandler == null)
            return;

        if (elementType == ElementType.Ice && entityStatusHandler.CanEffectBeApplied(ElementType.Ice))
            entityStatusHandler.ApplyChilledEffect(defaultDuration, chilledSlowMultiplier);

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
