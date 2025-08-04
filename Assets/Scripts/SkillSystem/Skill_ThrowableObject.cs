using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ThrowableObject : Skill_Base
{
    [Header("Prefab & Parameters")]
    [Tooltip("Prefab must have a SkillObject_ThrowableObject on its root.")]
    [SerializeField] private GameObject throwableObjectPrefab;
    [Range(0f, 20f)]
    [SerializeField] private float throwSpeed    = 10f;  // horizontal speed (units/sec)
    [SerializeField] private float baseFlightTime= 1f;   // seconds until landing
    [SerializeField] private float arcHeight     = 2f;   // max vertical lift

    [Header("Trajectory Preview Dots")]
    [Tooltip("Simple dot prefab (e.g. small sprite) to show the ground path.")]
    [SerializeField] private GameObject predictionDotPrefab;
    [SerializeField] private int    numberOfDots    = 20;
    [SerializeField] private float  timeBetweenDots = 0.05f;

    // Cached for preview + throw
    private Transform[] dots;
    private Vector2      confirmedDirection;
    private float confirmedDistance;
    private float confirmedFlightTime;

    // Expose for the thrown‐object setup
    public float ThrowSpeed => throwSpeed;
    public float FlightTime => baseFlightTime;
    public float ArcHeight  => arcHeight;

    protected override void Awake()
    {
        base.Awake();

        // Pre‐instantiate all your dots and disable them
        dots = new Transform[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            var dot = Instantiate(predictionDotPrefab, transform);
            dot.SetActive(false);
            dots[i] = dot.transform;
        }
    }

    /// <summary>
    /// Called from your ThrowingState when the player releases the throw button.
    /// </summary>
    public void ThrowObject()
    {
        // Spawn the projectile at your player’s position
        var obj = Instantiate(throwableObjectPrefab, transform.position, Quaternion.identity);
        if (obj.TryGetComponent<SkillObject_ThrowableObject>(out var comp))
        {
            // pass the per‐throw time instead of manager.FlightTime
            comp.SetupThrowableObject(this,
                                     confirmedDirection,
                                     confirmedFlightTime);
        }
    }

    /// <summary>
    /// As the player aims, call this every frame to move the dots along the ground path.
    /// </summary>
    public void PredictTrajectory(Vector2 direction)
    {
        Vector2 origin   = (Vector2)transform.position;
        float   maxDist  = throwSpeed * baseFlightTime;

        // previewDistance matches the clamp logic above
        float previewDist = Mathf.Min(
            Vector2.Distance(origin, Camera.main.ScreenToWorldPoint(playerBrain.mousePosition)),
            maxDist
        );

        // previewTime is based on that distance
        float previewTime = previewDist / throwSpeed;

        for (int i = 0; i < numberOfDots; i++)
        {
            // EVENLY space tNorm from 0→1
            float tNorm = (float)i / (numberOfDots - 1);

            // ground position at this fraction of the throw
            Vector2 groundPos = origin + direction.normalized * (previewDist * tNorm);

            // same arc formula (will collapse to 0 if arcHeight==0)
            float height   = 4f * arcHeight * tNorm * (1f - tNorm);
            float scale    = 1f + height * 0.1f;

            dots[i].position   = groundPos;
            dots[i].localScale = Vector3.one * scale;
            dots[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Store the final direction & flightTime so the throw lands exactly where the mouse is,
    /// but no farther than max range.
    /// </summary>
    public void ConfirmTrajectory(Vector2 dir)
    {
        Vector2 origin = (Vector2)transform.position;
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(playerBrain.mousePosition);
        float maxDist = throwSpeed * baseFlightTime;
        float distToMouse = Vector2.Distance(origin, mouseWorld);

        // 1) clamp distance (so you don’t exceed max range)
        confirmedDistance = Mathf.Min(distToMouse, maxDist);

        // 2) compute the exact flight time you need
        confirmedFlightTime = confirmedDistance / throwSpeed;

        // 3) store direction as usual
        confirmedDirection = dir.normalized;
    }


    /// <summary>
    /// Toggle all your preview dots on/off.
    /// </summary>
    public void EnableDots(bool enable)
    {
        foreach (var d in dots)
            d.gameObject.SetActive(enable);
    }


}
