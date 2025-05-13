using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header ("Flash FX  Settings")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float flashDuration = 0.15f;
    private Material originalMaterial;
 

    private void Start()
    {
     sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMaterial;
    }
}
