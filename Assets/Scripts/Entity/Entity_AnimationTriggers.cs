using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    protected Entity_Brain entityBrain;
    protected Entity_Combat entityCombat;

    protected virtual void Awake()
    {
        entityBrain = GetComponentInParent<Entity_Brain>();
        entityCombat = GetComponentInParent<Entity_Combat>();
    }

    private void CallAnimationFinishTrigger()
    {
        entityBrain?.CallAnimationFinishTrigger();
    }

    private void CallAnimationDebug()
    {
       Debug.Log("Animation Playerd");
    }

    private void CallAnimationAttackTrigger()
    {
        entityCombat?.PerformAttack();
    }
}
