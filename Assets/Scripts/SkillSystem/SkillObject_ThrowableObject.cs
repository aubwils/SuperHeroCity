using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_ThrowableObject : SkillObject_Base
{
    protected Rigidbody2D rb;
    protected Skill_ThrowableObject throwableObjectManager;

    public virtual void SetupThrowableObject(Skill_ThrowableObject throwableObjectManager, Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction; // dnot think this will work with 2d top down.... not using gravity in game.

        this.throwableObjectManager = throwableObjectManager;

        playerStats = throwableObjectManager.playerBrain.entityStats;
        damageScaleData = throwableObjectManager.damageScaleData;
    }
}
