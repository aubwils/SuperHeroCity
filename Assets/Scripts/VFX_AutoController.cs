using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float autoDestroyDelay = 1f;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;
    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 360f;
    
    [Header("Random Position") ]
    [SerializeField] private float xMinOffset = -0.15f;
    [SerializeField] private float xMaxOffset = 0.15f;
    [Space]
    [SerializeField] private float yMinOffset = -0.15f;
    [SerializeField] private float yMaxOffset = 0.15f;

    private void Start()
    {
        ApplyRandomOffset();
        ApplyRansomRotation();

        if (autoDestroy)
            Destroy(gameObject, autoDestroyDelay);
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
