using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drives a top‐down “arc” throw by splitting horizontal (ground) vs. vertical (height) movement.
/// </summary>
public class SkillObject_ThrowableObject : SkillObject_Base
{
     // these are set by the skill when instantiated:
    private float throwSpeed;
    private float flightTime;   // seconds in air
    private float arcHeight;    // vertical lift at full normalized
    private float maxDistance;  // ground distance to travel

    private Vector2 startPos;
    private Vector2 direction;
    private float   elapsedTime;

    [Tooltip("Child Transform containing your sprite or shadow.")]
    [SerializeField] private Transform spriteTransform;

    /// <summary>
    /// manager:  the Skill_ThrowableObject component  
    /// dir:      normalized throw direction  
    /// time:     seconds the object remains in flight  
    /// dist:     ground distance to travel  
    /// </summary>
    public void SetupThrowableObject(
        Skill_ThrowableObject manager,
        Vector2                  dir,
        float                     time,
        float                     dist
    )
    {
        // cache values
        throwSpeed   = manager.ThrowSpeed;
        flightTime   = time;
        maxDistance  = dist;

        // scale arc height by normalized distance (0 at point-blank → 1 at full range)
        arcHeight    = manager.MaxArcHeight * Mathf.Clamp01(dist / manager.ThrowRange);

        startPos     = transform.position;
        direction    = dir;
        elapsedTime  = 0f;

        // disable built-in physics
        if (TryGetComponent<Rigidbody2D>(out var rb))
            rb.isKinematic = true;

        // cache for damage logic
        playerStats     = manager.playerBrain.entityStats;
        damageScaleData = manager.damageScaleData;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / flightTime;

        if (t >= 1f)
        {
            // snap to final ground position, then impact
            transform.position = startPos + direction * maxDistance;
            OnImpact();
            return;
        }

        // 1) ground-plane movement
        Vector2 pos = startPos + direction * (maxDistance * t);
        transform.position = pos;

        // 2) vertical lift (parabola)
        if (arcHeight > 0f)
        {
            float height = 4f * arcHeight * t * (1f - t);
            spriteTransform.localPosition = new Vector3(0f, height, 0f);
        }
        else
        {
            spriteTransform.localPosition = Vector3.zero;
        }
    }

    private void OnImpact()
    {
        // apply damage, VFX, etc.
        DamageEnemiesInRadius(transform, damageRadius);
        Destroy(gameObject);
    }

    // Different characters could have different arc styles:
    // - Superman: High, powerful arcs
    // - Batman: Lower, precise arcs  
    // - Speedster: Nearly straight (speed over strength)
}
