using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationTriggers : Entity_AnimationTriggers
{
    private Player_Brain playerBrain;

    protected override void Awake()
    {
        base.Awake();
        playerBrain = GetComponentInParent<Player_Brain>();
    }

    private void ThrowingObject() => playerBrain.skillManager.throwableObject.ThrowObject();

  
}
