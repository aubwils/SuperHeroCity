using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_Stats entityStats;

    public DamageScaleData basicAttackScale;

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
        foreach (var target in GetDetectedColliders())
        {

            if (target.TryGetComponent(out IDamageable damageable))
            {
                AttackData attackData = entityStats.GetAttackData(basicAttackScale);
                Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

                float physicalDamage = attackData.physicalDamage;
                float elementalDamage = attackData.elementalDamage;
                ElementType elementType = attackData.element;
                
                damageable.TakeDamage(physicalDamage, elementalDamage, elementType, transform);

                if (elementType != ElementType.None)
                    statusHandler?.ApplyStatusEffect(elementType, attackData.effectData);

                if (TryGetComponent(out Entity_VFX vfx))
                {
                    vfx.UpdateOnHitColorForElementColor(elementType);
                    Vector2 facingDir = GetComponent<Entity_Brain>().GetFacingDirection(); ;
                    vfx.PlayHitVFX(facingDir, attackData.isCrit);
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
