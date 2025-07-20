using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float autoDestroyDelay = 1f;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Fade Effect")]
    [SerializeField] private bool canFade;
    [SerializeField] private float fadeSpeed = 1f;


    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 360f;
    
    [Header("Random Position") ]
    [SerializeField] private float xMinOffset = -0.15f;
    [SerializeField] private float xMaxOffset = 0.15f;
    [Space]
    [SerializeField] private float yMinOffset = -0.15f;
    [SerializeField] private float yMaxOffset = 0.15f;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (canFade)
            StartCoroutine(FadeRoutine());

        ApplyRandomOffset();
        ApplyRansomRotation();

        if (autoDestroy)
            Destroy(gameObject, autoDestroyDelay);
    }

    private IEnumerator FadeRoutine()
    {
        Color targetColor = Color.white;

        while (targetColor.a > 0)
        {
            targetColor.a = targetColor.a - (fadeSpeed * Time.deltaTime);
            sr.color = targetColor;
            yield return null;
        }
        sr.color = targetColor;
    }

    private void ApplyRandomOffset()
    {
        if (randomOffset == false)
            return;

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset, yMaxOffset);

        transform.position = transform.position + new Vector3(xOffset, yOffset);
    }
    
    private void ApplyRansomRotation()
    {
        if (randomRotation == false)
            return;

        float zRotation = Random.Range(minRotation, maxRotation);
        transform.Rotate(0, 0, zRotation);
    }
}
