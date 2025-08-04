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
        var obj = Instantiate(throwableObjectPrefab, dots[1].position, Quaternion.identity);
        if (obj.TryGetComponent<SkillObject_ThrowableObject>(out var comp))
        {
            comp.SetupThrowableObject(this, confirmedDirection);
        }
    }

    /// <summary>
    /// As the player aims, call this every frame to move the dots along the ground path.
    /// </summary>
    public void PredictTrajectory(Vector2 direction)
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            float t = timeBetweenDots * i;
            // Clamp so t never exceeds your flight time
            t = Mathf.Min(t, baseFlightTime);

            // Ground‐only position: start + dir * (speed * t)
            Vector2 pos = (Vector2)transform.position
                        + direction.normalized * (throwSpeed * t);

            dots[i].position = pos;
            dots[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Store the final direction to pass into ThrowObject().
    /// </summary>
    public void ConfirmTrajectory(Vector2 direction)
    {
        confirmedDirection = direction.normalized;
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
