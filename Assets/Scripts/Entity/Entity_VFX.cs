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
}