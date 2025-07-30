using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_Portal : SkillObject_Base
{

    [SerializeField] private GameObject vfxPrefab;

    public void SetupPortal(float portalExistDuration)
    {
        Invoke(nameof(Disappear), portalExistDuration);
    }

    
    public void Disappear()
    {
        // should do a animation dissaper and then destroy on animation finisher.
        Destroy(gameObject);
    }

}
