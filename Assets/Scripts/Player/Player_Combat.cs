using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter Attack Details")]
    [SerializeField] private float counterRecoveryDuration =.1f;

    public bool CounterAttackPreformed()
    {
        bool hasCounteredSomebody = false;
        foreach (var collider in GetDetectedColliders())
        {
            ICounterable counterable = collider.GetComponent<ICounterable>();

            if (counterable == null)
                continue;

            if (counterable.CanBeCountered)
            {
                counterable.HandleCounterAttacks();
                hasCounteredSomebody = true;
            }
                
        }
        return hasCounteredSomebody;
    }

    public float GetCounterRecoveryDuration() => counterRecoveryDuration;

}

