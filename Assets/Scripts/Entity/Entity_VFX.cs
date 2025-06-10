using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("On Damage VFX")]
    [SerializeField] private Material OnDamageMaterial;
    [SerializeField] private float OnDamageVFXDuration = 0.2f;
    private Material origionalMaterial;
    private Coroutine OnDamageVFXCoroutine;

    [Header("Hit VFX")]
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private float hitVFXDuration = 0.3f;
    [SerializeField] private float offsetDistance = 0.5f;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        origionalMaterial = sr.material;
    }

    public void PlayOnDamageVFX()
    {
        if (OnDamageVFXCoroutine != null)
        {
            StopCoroutine(OnDamageVFXCoroutine);
        }
        OnDamageVFXCoroutine = StartCoroutine(OnDamageVFXCo());
    }

    private IEnumerator OnDamageVFXCo()
    {
        sr.material = OnDamageMaterial;
        yield return new WaitForSeconds(OnDamageVFXDuration);
        sr.material = origionalMaterial;
    }

    public void PlayHitVFX(Vector2 facingDirection)
    {
        if (hitVFXPrefab == null) return;

        Vector3 offset = (Vector3)(facingDirection.normalized * offsetDistance);
        Vector3 spawnPosition = transform.position + offset;

        GameObject hitVFX = Instantiate(hitVFXPrefab, spawnPosition, Quaternion.identity);
        //FUTURE: look into using Object Pooling for hit VFX
        Destroy(hitVFX, hitVFXDuration);
        
    }
}