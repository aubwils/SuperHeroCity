using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private Material hitMaterial;
    private Material origionalMaterial;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        origionalMaterial = sr.material;
    }
    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;
        yield return new WaitForSeconds(flashDuration);
        sr.material = origionalMaterial;
    }
}
