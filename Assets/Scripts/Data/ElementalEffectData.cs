using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ElementalEffectData
{
    public float chillDuration;
    public float chilledSlowMultiplier;

    public float burnDuration;
    public float burnDamage;

    public float shockDuration;
    public float shockDamage;
    public float shockCharge;

    public ElementalEffectData(Entity_Stats entityStats, DamageScaleData damageScale)
    {
        chillDuration = damageScale.chillDuration;
        chilledSlowMultiplier = damageScale.chilledSlowMultiplier;

        burnDuration = damageScale.burnDuration;
        burnDamage = entityStats.offenseStats.fireDamage.GetValue() * damageScale.burnDamageScale;

        shockDuration = damageScale.shockDuration;
        shockDamage = entityStats.offenseStats.fireDamage.GetValue() * damageScale.shockDamageScale;
        shockCharge = damageScale.shockCharge;

        
    }
}
