using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject onDestroyEffectPrefab;

    public bool TakeDamage(float damage, Transform damageSource)
    {
        Instantiate(onDestroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        return true;
    }
}
