using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ThrowableObject : Skill_Base
{
    [Header("Prefab & Parameters")]
    [Tooltip("Prefab must have a SkillObject_ThrowableObject on its root.")]
    [SerializeField] private GameObject throwableObjectPrefab;

    [Header("Throw Settings")]
    [Range(0f, 20f)]
    [Tooltip("How fast (units/sec) the object travels.")]
    [SerializeField] private float throwSpeed = 10f;
    [Tooltip("Seconds to travel the full throwRange.")]
    [SerializeField] private float baseFlightTime = 1f;
    [Tooltip("Max ground-distance the player can throw.")]
    [SerializeField] private float throwRange = 8f;
    [Tooltip("Vertical lift at max range (0 = straight).")]
    [SerializeField] private float maxArcHeight = 2f;

    [Header("Trajectory Preview Dots")]
    [Tooltip("Small sprite—scale it in its own prefab.")]
    [SerializeField] private GameObject predictionDotPrefab;
    [SerializeField] private int maxDots = 20;      // cap on preview circles
    [SerializeField] private float dotSpacing = 0.5f;    // world units between dots

    // runtime state
    private Transform[] dots;
    private Vector2 confirmedDirection;
    private float confirmedDistance;
    private float confirmedFlightTime;

    // make these available for the spawned object
    public float ThrowSpeed => throwSpeed;
    public float BaseFlightTime => baseFlightTime;
    public float ThrowRange => throwRange;
    public float MaxArcHeight => maxArcHeight;

    protected override void Awake()
    {
        base.Awake();
        // pool our dot instances
        dots = new Transform[maxDots];
        for (int i = 0; i < maxDots; i++)
        {
            var d = Instantiate(predictionDotPrefab, transform);
            d.SetActive(false);
            dots[i] = d.transform;
        }
    }

    /// <summary>Show N dots evenly—no more than maxDots—clamped by throwRange.</summary>
    public void PredictTrajectory(Vector2 direction)
    {
        Vector2 origin = (Vector2)transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(playerBrain.mousePosition);

        // 1) clamp ground distance against throwRange
        float desiredDist = Vector2.Distance(origin, mousePos);
        float actualDist = Mathf.Min(desiredDist, throwRange);

        // 2) landing spot
        Vector2 targetPos = origin + direction.normalized * actualDist;

        // 3) how many dots?
        int dotCount = Mathf.Clamp(
            Mathf.CeilToInt(actualDist / dotSpacing),
            1,
            maxDots
        );

        // 4) place each dot 0→1 fraction along origin→target
        for (int i = 0; i < maxDots; i++)
        {
            if (i < dotCount)
            {
                float tNorm = (dotCount == 1) ? 1f : (float)i / (dotCount - 1);
                Vector2 worldPos = Vector2.Lerp(origin, targetPos, tNorm);
                dots[i].position = worldPos;
                dots[i].gameObject.SetActive(true);
            }
            else
            {
                dots[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Call on button‐release: determine final distance & flight time.
    /// </summary>
    public void ConfirmTrajectory(Vector2 direction)
    {
        Vector2 origin = (Vector2)transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(playerBrain.mousePosition);

        float desiredDist = Vector2.Distance(origin, mousePos);
        confirmedDistance = Mathf.Min(desiredDist, throwRange);

        // shorter throws take proportionally less time
        confirmedFlightTime = baseFlightTime * (confirmedDistance / throwRange);
        confirmedDirection = direction.normalized;
    }

    /// <summary>Spawn the actual projectile with your computed params.</summary>
    public void ThrowObject()
    {
        var obj = Instantiate(throwableObjectPrefab, transform.position, Quaternion.identity);
        if (obj.TryGetComponent<SkillObject_ThrowableObject>(out var comp))
        {
            comp.SetupThrowableObject(
                this,
                confirmedDirection,
                confirmedFlightTime,
                confirmedDistance
            );
        }
    }

    /// <summary>Turn all preview dots on or off.</summary>
    public void EnableDots(bool enable)
    {
        foreach (var d in dots)
            d.gameObject.SetActive(enable);
    }
    
    private void OnDrawGizmosSelected()
    {
        // only draw if we have a valid range
        if (throwRange > 0f)
        {
            Gizmos.color = Color.cyan;  
            Gizmos.DrawWireSphere(transform.position, throwRange);
        }
    }
}