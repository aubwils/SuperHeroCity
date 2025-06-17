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

    private void Awake()
    {
        entityBrain = GetComponent<Entity_Brain>();
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
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
        return currentElementType == ElementType.None;
    }

}
