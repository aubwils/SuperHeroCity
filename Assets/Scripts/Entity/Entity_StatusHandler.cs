using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementType currentElementType = ElementType.None;
    private Entity_Brain entityBrain;
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;

    [Header("Electrify effect details")]
    [SerializeField] private GameObject lightningStrikeVFXPrefab;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge = 1f;
    private Coroutine shockedEffectRoutine;

    private void Awake()
    {
        entityBrain = GetComponent<Entity_Brain>();
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
        entityHealth = GetComponent<Entity_Health>();
    }

    public void ApplyStatusEffect(ElementType element, ElementalEffectData effectData)
    {
        if (element == ElementType.Ice && CanEffectBeApplied(ElementType.Ice))
            ApplyChilledEffect(effectData.chillDuration, effectData.chilledSlowMultiplier);

        if (element == ElementType.Fire && CanEffectBeApplied(ElementType.Fire))
            ApplyBurnEffect(effectData.burnDuration, effectData.burnDamage);

        if (element == ElementType.Lightning && CanEffectBeApplied(ElementType.Lightning))
            ApplyShockEffect(effectData.shockDuration, effectData.shockDamage, effectData.shockCharge);
        
        //need to add in posion and other damage types.
        
    }

    public void ApplyShockEffect(float duration, float damage, float charge)
    {
        //dont think i like the build charge up  but maybe useful for a weapon? keepign for now may remove.
        float lightningResistance = entityStats.GetElementalResistance(ElementType.Lightning);
        float additionalCharge = charge * (1 - lightningResistance);

        currentCharge = currentCharge + additionalCharge;
        if (currentCharge >= maxCharge)
        {
            LightingStrike(damage);
            StopShockEffect();
            return;
        }

        if (shockedEffectRoutine != null)
            StopCoroutine(shockedEffectRoutine);


        shockedEffectRoutine = StartCoroutine(ShockedEffectRoutine(duration));

    }
    private void StopShockEffect()
    {
        currentCharge = 0f;
        currentElementType = ElementType.None;
        entityVFX.StopAllVFX();
    }

    private void LightingStrike(float damage)
    {
        Instantiate(lightningStrikeVFXPrefab, transform.position, Quaternion.identity);
        entityHealth.ReduceHealth(damage);
        DamageTextSpawnerManager.Instance.SpawnDamageText(Mathf.RoundToInt(damage), transform);
    }
    private IEnumerator ShockedEffectRoutine(float duration)
    {
        currentElementType = ElementType.Lightning;
        entityVFX.PlayStatusVFX(duration, ElementType.Lightning);

        yield return new WaitForSeconds(duration);
        StopShockEffect();
    }

    public void ApplyBurnEffect(float duration, float fireDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
        float reducedFireDamage = fireDamage * (1 - fireResistance);

        StartCoroutine(BurnEffectRoutine(duration, reducedFireDamage));
    }

    private IEnumerator BurnEffectRoutine(float duration, float totalDamage)
    {
        currentElementType = ElementType.Fire;
        entityVFX.PlayStatusVFX(duration, ElementType.Fire);
        int ticksPerSecond = 2; // Number of ticks per second. Ticks means instance of damage while target is burning.
        int tickCount = Mathf.RoundToInt(duration * ticksPerSecond); // Total number of ticks

        float damagePerTick = totalDamage / tickCount; // Total damage divided by number of ticks
        float tickInterval = 1f / ticksPerSecond; // 0.5 seconds per tick

        for (int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHealth(damagePerTick);
            DamageTextSpawnerManager.Instance.SpawnDamageText(Mathf.RoundToInt(damagePerTick), transform);
            yield return new WaitForSeconds(tickInterval);
        }
        currentElementType = ElementType.None;
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier)
    {
        float iceResistance = entityStats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectRoutine(reducedDuration, slowMultiplier));
    }

    private IEnumerator ChilledEffectRoutine(float duration, float slowMultiplier)
    {
        entityBrain.SlowDownEntity(duration, slowMultiplier);
        currentElementType = ElementType.Ice;
        entityVFX.PlayStatusVFX(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);
        currentElementType = ElementType.None;
    }

    public bool CanEffectBeApplied(ElementType newElementType)
    {
        if(newElementType == ElementType.Lightning && currentElementType == ElementType.Lightning)
            return true; 
        
        return currentElementType == ElementType.None;
    }

}
