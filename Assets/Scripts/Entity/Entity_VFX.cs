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
    [SerializeField] private Color hitVFXColor = Color.white;
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private GameObject critHitVFXPrefab;
    [SerializeField] private float offsetDistance = 0.5f;

    [Header("Element Colors")]
    [SerializeField] private Color chillVFXColor = Color.cyan;
    [SerializeField] private Color burnVFXColor = Color.red;
    [SerializeField] private Color shockVFXColor = Color.yellow;
    [SerializeField] private Color poisonVFXColor = Color.green;
    [SerializeField] private Color holyVFXColor = Color.white;
    [SerializeField] private Color darkVFXColor = Color.black;
    
    private Color origionalHitVFXColor;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        origionalMaterial = sr.material;
        origionalHitVFXColor = hitVFXColor;
    }
    public void PlayStatusVFX(float duration, ElementType elementType)
    {
        if (elementType == ElementType.Ice)
            StartCoroutine(PlayStatusVFXRoutine(duration, chillVFXColor));
        if (elementType == ElementType.Fire)
            StartCoroutine(PlayStatusVFXRoutine(duration, burnVFXColor));
        if (elementType == ElementType.Lightning)
            StartCoroutine(PlayStatusVFXRoutine(duration, shockVFXColor));
        //if (elementType == ElementType.Poison)
          //  StartCoroutine(PlayStatusVFXRoutine(duration, poisonVFXColor));
        // if (elementType == ElementType.Holy)
        //     StartCoroutine(PlayStatusVFXRoutine(duration, holyVFXColor));
        // if (elementType == ElementType.Dark)
        //     StartCoroutine(PlayStatusVFXRoutine(duration, darkVFXColor));
    }
    public void StopAllVFX()
    {
        StopAllCoroutines();
        sr.color = origionalHitVFXColor; // Reset the color to original
        sr.material = origionalMaterial; // Reset the material to original
    }
    private IEnumerator PlayStatusVFXRoutine(float duration, Color effectColor)
    {
        float timePassed = 0f;
        float tickInterval = .25f;
        Color lightColor = effectColor * 1.2f;
        Color darkColor = effectColor * 0.8f;
        Color origionalColor = sr.color;

        bool toggle = false;

        while (timePassed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(tickInterval);
            timePassed += tickInterval;
        }

        sr.color = origionalColor;
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

    public void PlayHitVFX(Vector2 facingDirection, bool isCrit)
    {
        if (hitVFXPrefab == null) return;

        Vector3 offset = (Vector3)(facingDirection.normalized * offsetDistance);
        Vector3 spawnPosition = transform.position + offset;

        GameObject hitPrefab = isCrit ? critHitVFXPrefab : hitVFXPrefab;

        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject hitVFX = Instantiate(hitPrefab, spawnPosition, rotation);
        hitVFX.GetComponentInChildren<SpriteRenderer>().color = hitVFXColor;
        //FUTURE: look into using Object Pooling for hit VFX    
    }

    public void UpdateOnHitColorForElementColor(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.None:
                hitVFXColor = origionalHitVFXColor;
                break;
            case ElementType.Ice:
                hitVFXColor = chillVFXColor;
                break;
            default:
                hitVFXColor = origionalHitVFXColor;
                break;
        }
    }

    
}