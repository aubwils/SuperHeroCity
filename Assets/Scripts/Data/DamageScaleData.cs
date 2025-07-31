using UnityEngine;
using System;

[Serializable]
public class DamageScaleData
{
    [Header("Damage")]
    public float physical = 1f;
    public float elemental = 1f;

    [Header("Chill")]
    public float chillDuration = 3f;
    public float chilledSlowMultiplier = .2f;

    [Header("Burn")]
    public float burnDuration = 3f;
    public float burnDamageScale = 1f;

    [Header("Shock")]
    public float shockDuration = 3f;
    public float shockDamageScale = 1f;
    public float shockCharge = .4f; // 1 means one hit to trigger a charge, .4 is about 3 hits.

    // Scale factor can be used to adjust the final damage output, e.g. for balancing purposes
    //1f is full damage, 0.5f is half damage, etc. good usecase is if clones do half damage as the original entity.
    // can us this one. asword throw attack that does 200% damage, you can use 2f as the scale factor.
    //or regular attack do 60% and magic attack do 100% damage.

}
