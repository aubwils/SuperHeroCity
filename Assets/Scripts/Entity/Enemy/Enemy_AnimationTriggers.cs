using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationTriggers
{   
    private Enemy_Brain enemyBrain;
    private Enemy_VFX enemyVFX;

    protected override void Awake()
    {
        base.Awake();
        enemyBrain = GetComponentInParent<Enemy_Brain>();
        enemyVFX = GetComponentInParent<Enemy_VFX>();
    }

    private void EnableCounterWindow()
    {
        enemyVFX.EnableAttackAlert(true);
        //In future i think i want to remove the attack alert image on 
        // the counter window and just have it be based on enemy animation
        // but the attack alert i think i would keep for if player is spotted and enemy starts chasing you on first chase
        enemyBrain.EnableCounterWindow(true);
    }
    private void DisableCounterWindow()
    {
        enemyVFX.EnableAttackAlert(false);
        enemyBrain.EnableCounterWindow(false);
    }

}
