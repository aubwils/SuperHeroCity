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
    [SerializeField] private float electrifiedChargeBuildup = .4f;
    


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

                float elementalDamage = entityStats.GetElementalDamage(out ElementType elementType, 1f);
                // Scale factor can be used to adjust the final damage output, e.g. for balancing purposes
                //1f is full damage, 0.5f is half damage, etc. good usecase is if clones do half damage as the original entity.
                //or regular attack do 60% and magic attack do 100% damage.
                //can remove the 1f here since default is 1f, but leaving it here for clarity.


                float damage = entityStats.GetPhisicalDamage(out bool isCrit, 1f);
                // Scale factor can be used to adjust the final damage output, e.g. for balancing purposes
                //1f is full damage, 0.5f is half damage, etc.
                // can us this one. asword throw attack that does 200% damage, you can use 2f as the scale factor.
                //can remove the 1f here since default is 1f, but leaving it here for clarity.
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

    public void ApplyStatusEffect(Transform target, ElementType elementType, float scaleFactor = 1f)
    {

        Entity_StatusHandler entityStatusHandler = target.GetComponent<Entity_StatusHandler>();
        if (entityStatusHandler == null)
            return;

        if (elementType == ElementType.Ice && entityStatusHandler.CanEffectBeApplied(ElementType.Ice))
            entityStatusHandler.ApplyChilledEffect(defaultDuration, chilledSlowMultiplier);

        if (elementType == ElementType.Fire && entityStatusHandler.CanEffectBeApplied(ElementType.Fire))
        {
            float fireDamage = entityStats.offenseStats.fireDamage.GetValue() * scaleFactor;
            entityStatusHandler.ApplyBurnEffect(defaultDuration, fireDamage);
        }
        if (elementType == ElementType.Lightning && entityStatusHandler.CanEffectBeApplied(ElementType.Lightning))
        {
            float lightningDamage = entityStats.offenseStats.lightningDamage.GetValue() * scaleFactor;
            entityStatusHandler.ApplyElectrifiedEffect(defaultDuration, lightningDamage, electrifiedChargeBuildup);
        }// coem back to lightning think want a diff thing with this...

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
