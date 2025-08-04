using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drives a top‐down “arc” throw by splitting horizontal (ground) vs. vertical (height) movement.
/// </summary>
public class SkillObject_ThrowableObject : SkillObject_Base
{
    //–– Configuration passed in from the skill
    private float throwSpeed;    // horizontal units/sec
    private float flightTime;    // total seconds in air
    private float arcHeight;     // max “lift” in world units
    private float maxDistance;   // throwSpeed * flightTime

    //–– Runtime tracking
    private Vector2 startPos;    // ground‐plane origin
    private Vector2 direction;   // normalized throw direction
    private float elapsedTime;   // how long we’ve been flying

    //–– Sprite child to lift up/down, so physics/colliders stay at ground level
    [Tooltip("Assign the child transform containing your character sprite or shadow.")]
    [SerializeField] private Transform spriteTransform;

    //–– Reference back to the manager for stats/damage scaling
    private Skill_ThrowableObject manager;

    /// <summary>
    /// Called by the skill after instantiating this prefab.
    /// Caches parameters and turns off Rigidbody simulation.
    /// </summary>
    public void SetupThrowableObject(
      Skill_ThrowableObject manager,
      Vector2 dir,
      float    flightTimeOverride
    ) {
        throwSpeed   = manager.ThrowSpeed;
        flightTime   = flightTimeOverride;     // ← use the override
        arcHeight    = manager.ArcHeight;
        maxDistance  = throwSpeed * flightTime; // ← recompute for this throw

        startPos     = transform.position;
        direction    = dir.normalized;
        elapsedTime  = 0f;

        var rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        // … cache stats as before …
    }

    private void Update()
    {
        UpdateArcMovement();
    }

    /// <summary>
    /// Moves the object along a parabola until it “lands.”
    /// </summary>
    private void UpdateArcMovement()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / flightTime;     // ← uses the override now

        if (t >= 1f) { OnImpact(); return; }

        Vector2 groundPos = startPos + direction * (maxDistance * t);
        transform.position = groundPos;

        if (arcHeight > 0f)  // optional: skip arc logic if height==0
        {
            float height = 4f * arcHeight * t * (1f - t);
            spriteTransform.localScale = Vector3.one * (1f + height * 0.1f);
        }
        else
        {
            spriteTransform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Cleanup / impact logic here (explosion, damage, VFX, etc.).
    /// </summary>
    private void OnImpact()
    {
        // TODO: Deal damage or spawn effects via your manager or VFX system
        Destroy(gameObject);
    }

    // Different characters could have different arc styles:
    // - Superman: High, powerful arcs
    // - Batman: Lower, precise arcs  
    // - Speedster: Nearly straight (speed over strength)
}
