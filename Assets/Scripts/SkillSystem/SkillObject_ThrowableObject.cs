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
    public void SetupThrowableObject(Skill_ThrowableObject manager, Vector2 dir)
    {
        this.manager = manager;
        throwSpeed = manager.ThrowSpeed;
        flightTime = manager.FlightTime;
        arcHeight = manager.ArcHeight;
        maxDistance = throwSpeed * flightTime;

        startPos = transform.position;
        direction = dir.normalized;
        elapsedTime = 0f;

        // Stop Unity physics so we can move manually
        var rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        // Cache stats for damage scaling (from your base class)
        playerStats = manager.playerBrain.entityStats;
        damageScaleData = manager.damageScaleData;
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
        float t = elapsedTime / flightTime;

        // Once t ≥ 1, trigger impact and destroy
        if (t >= 1f)
        {
            OnImpact();
            return;
        }

        // 1) Ground position: linear interpolation from start → maxDistance
        Vector2 groundPos = startPos + direction * (maxDistance * t);
        transform.position = groundPos;

        // 2) Height offset: h(t) = 4 * H * t * (1 – t)
        float height = 4f * arcHeight * t * (1f - t);

        // 3) Lift just the sprite (keep colliders at ground level)
        if (spriteTransform != null)
        {
            spriteTransform.localPosition = new Vector3(0f, height, 0f);
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
