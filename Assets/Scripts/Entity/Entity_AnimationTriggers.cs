using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity_Brain entityBrain;
    private Entity_Combat entityCombat;

    private void Awake()
    {
        entityBrain = GetComponentInParent<Entity_Brain>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }

    private void CallAnimationFinishTrigger()
    {
        entityBrain?.CallAnimationFinishTrigger();
    }

    private void CallAnimationAttackTrigger()
    {
        entityCombat?.PerformAttack();
    }
}
