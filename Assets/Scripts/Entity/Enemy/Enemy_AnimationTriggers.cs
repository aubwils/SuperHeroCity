using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationTriggers
{   
    private Enemy_Brain enemyBrain;

    protected override void Awake()
    {
        base.Awake();
        enemyBrain = GetComponentInParent<Enemy_Brain>();
    }

    private void EnableCounterWindow()
    {
        enemyBrain.EnableCounterWindow(true);
    }
    private void DisableCounterWindow()
    {
        enemyBrain.EnableCounterWindow(false);
    }

}
