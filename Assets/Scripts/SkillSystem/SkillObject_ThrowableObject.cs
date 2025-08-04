using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drives a top‐down “arc” throw by splitting horizontal (ground) vs. vertical (height) movement.
/// </summary>
public class SkillObject_ThrowableObject : SkillObject_Base
{
    private float throwSpeed;
    private float flightTime;
    private float arcHeight;
    private float maxDistance;

    private Vector2 startPos;
    private Vector2 direction;
    private float   elapsedTime;

    [Tooltip("Child transform containing your sprite or shadow.")]
    [SerializeField] private Transform spriteTransform;

    /// <summary>
    /// manager:   the Skill_ThrowableObject that spawned this  
    /// dir:       normalized direction of the throw  
    /// time:      how long (seconds) it should stay aloft  
    /// dist:      how far it travels along the ground  
    /// </summary>
    public void SetupThrowableObject(
        Skill_ThrowableObject manager,
        Vector2                  dir,
        float                     time,
        float                     dist
    )
    {
        throwSpeed   = manager.ThrowSpeed;
        flightTime   = time;
        maxDistance  = dist;

        // scale arc height by distance ratio (0→1)
        float fullRange   = manager.ThrowSpeed * manager.BaseFlightTime;
        float ratio       = Mathf.Clamp01(dist / fullRange);
        arcHeight        = manager.MaxArcHeight * ratio;

        startPos     = transform.position;
        direction    = dir;
        elapsedTime  = 0f;

        // disable physics bodies
        if (TryGetComponent<Rigidbody2D>(out var rb))
            rb.isKinematic = true;

        // cache for damage
        playerStats     = manager.playerBrain.entityStats;
        damageScaleData = manager.damageScaleData;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / flightTime;

        if (t >= 1f)
        {
            // snap to final ground pos
            transform.position = startPos + direction * maxDistance;
            OnImpact();
            return;
        }

        // ground move
        Vector2 ground = startPos + direction * (maxDistance * t);
        transform.position = ground;

        // vertical arc lift
        if (arcHeight > 0f)
        {
            float h = 4f * arcHeight * t * (1f - t);
            spriteTransform.localPosition = new Vector3(0f, h, 0f);
        }
        else
        {
            spriteTransform.localPosition = Vector3.zero;
        }
    }

    private void OnImpact()
    {
        DamageEnemiesInRadius(transform, damageRadius);
        Destroy(gameObject);
    }

    // Different characters could have different arc styles:
    // - Superman: High, powerful arcs
    // - Batman: Lower, precise arcs  
    // - Speedster: Nearly straight (speed over strength)
}
