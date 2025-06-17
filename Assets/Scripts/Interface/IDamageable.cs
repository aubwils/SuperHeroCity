using UnityEngine;

public interface IDamageable
{
    bool TakeDamage(float damage, float elementalDamage, ElementType elementType, Transform damageSource);

}