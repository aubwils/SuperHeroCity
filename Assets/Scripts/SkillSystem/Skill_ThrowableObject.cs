using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ThrowableObject : Skill_Base
{
    [Header("Prefab & Parameters")]
    [Tooltip("Prefab must have a SkillObject_ThrowableObject on its root.")]
    [SerializeField] private GameObject throwableObjectPrefab;

    [Header("Throw Settings")]
    [Tooltip("How fast (units/sec) the object travels along the ground.")]
    [SerializeField] private float throwSpeed = 10f;
    [Tooltip("Max ground-distance the player can throw.")]
    [SerializeField] private float throwRange = 8f;
    [Tooltip("Vertical lift at max range (0 = straight line).")]
    [SerializeField] private float maxArcHeight = 2f;

    [Header("Trajectory Preview Dots")]
    [Tooltip("Prefab should be a small sprite; scale it in its own Inspector.")]
    [SerializeField] private GameObject predictionDotPrefab;
    [SerializeField] private int maxDots = 20;    // cap on dots
    [SerializeField] private float dotSpacing = 0.5f;  // world units between dots

    // runtime state for preview & throw
    private Transform[] dots;
    private Vector2 confirmedDirection;
    private float confirmedDistance;
    private float confirmedFlightTime;

    // Exposed for the projectile setup
    public float ThrowSpeed => throwSpeed;
    public float ThrowRange => throwRange;
    public float MaxArcHeight => maxArcHeight;

    protected override void Awake()
    {
        base.Awake();
        // Pre‐instantiate dot pool
        dots = new Transform[maxDots];
        for (int i = 0; i < maxDots; i++)
        {
            var d = Instantiate(predictionDotPrefab, transform);
            d.SetActive(false);
            dots[i] = d.transform;
        }
    }

    /// <summary>
    /// Call every frame while aiming to show dots.
    /// </summary>
    public void PredictTrajectory(Vector2 direction)
    {
        Vector2 origin = (Vector2)transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(playerBrain.mousePosition);

        // 1) clamp distance to throwRange
        float desired = Vector2.Distance(origin, mousePos);
        float actual = Mathf.Min(desired, throwRange);

        // 2) compute target position
        Vector2 target = origin + direction.normalized * actual;

        // 3) how many dots?
        int dotCount = Mathf.Clamp(
            Mathf.CeilToInt(actual / dotSpacing),
            1,
            maxDots
        );

        // 4) place dots evenly
        for (int i = 0; i < maxDots; i++)
        {
            if (i < dotCount)
            {
                float tNorm = (dotCount == 1) ? 1f : (float)i / (dotCount - 1);
                Vector2 pos = Vector2.Lerp(origin, target, tNorm);
                dots[i].position = pos;
                dots[i].gameObject.SetActive(true);
            }
            else
            {
                dots[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Call on release to finalize direction, distance & flight time.
    /// </summary>
    public void ConfirmTrajectory(Vector2 direction)
    {
        Vector2 origin = (Vector2)transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(playerBrain.mousePosition);

        float desired = Vector2.Distance(origin, mousePos);
        confirmedDistance = Mathf.Min(desired, throwRange);

        // time = distance ÷ speed
        confirmedFlightTime = confirmedDistance / throwSpeed;
        confirmedDirection = direction.normalized;
    }

    /// <summary>
    /// Spawn the projectile with your computed parameters.
    /// </summary>
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

    /// <summary>
    /// Toggle preview dots on/off.
    /// </summary>
    public void EnableDots(bool enable)
    {
        foreach (var d in dots)
            d.gameObject.SetActive(enable);
    }

    // ----------------------------------------------------------------------------
    // Gizmo: draws a wire circle for throwRange when selected in the Editor
    // ----------------------------------------------------------------------------
    private void OnDrawGizmosSelected()
    {
        if (throwRange > 0f)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, throwRange);
        }
    }
}
